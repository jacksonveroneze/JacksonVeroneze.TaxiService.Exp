# ################################################################################
# VPC
# ################################################################################

resource "aws_vpc" "vpc" {
  cidr_block           = var.vpc_cidr_block
  enable_dns_support   = var.vpc_enable_dns_support
  enable_dns_hostnames = var.vpc_enable_dns_hostnames
  tags                 = merge({ Name = var.vpc_name }, var.tags)
}

# ################################################################################
# IG
# ################################################################################

resource "aws_internet_gateway" "internet_gateway" {
  vpc_id = aws_vpc.vpc.id
  tags   = merge({ Name = var.internet_gateway_name }, var.tags)
}

# ################################################################################
# SUBNET
# ################################################################################

resource "aws_subnet" "subnet_public_1" {
  vpc_id                  = aws_vpc.vpc.id
  cidr_block              = var.subnet_public_1_cidr_block
  availability_zone       = var.subnet_public_1_az
  map_public_ip_on_launch = true
  tags                    = merge({ Name = var.subnet_public_1_name }, var.tags)
}

resource "aws_subnet" "subnet_private_1" {
  vpc_id            = aws_vpc.vpc.id
  cidr_block        = var.subnet_private_1_cidr_block
  availability_zone = var.subnet_private_1_az
  tags              = merge({ Name = var.subnet_private_1_name }, var.tags)
}

resource "aws_subnet" "subnet_private_2" {
  vpc_id            = aws_vpc.vpc.id
  cidr_block        = var.subnet_private_2_cidr_block
  availability_zone = var.subnet_private_2_az
  tags              = merge({ Name = var.subnet_private_2_name }, var.tags)
}

# ################################################################################
# ROUTE TABLE
# ################################################################################

resource "aws_route_table" "route_table" {
  vpc_id = aws_vpc.vpc.id

  route {
    cidr_block = "10.0.1.0/24"
    gateway_id = aws_internet_gateway.internet_gateway.id
  }

  tags = {
    Name = "example"
  }
}

resource "aws_route" "internet_access" {
  route_table_id         = aws_vpc.vpc.main_route_table_id
  destination_cidr_block = "0.0.0.0/0"
  gateway_id             = aws_internet_gateway.internet_gateway.id
}

resource "aws_route_table_association" "subnet_private_1" {
  subnet_id      = aws_subnet.subnet_private_1.id
  route_table_id = aws_route_table.route_table.id
}


# ################################################################################
# SECUTIRY GROUP
# ################################################################################

resource "aws_security_group" "lb" {
  name   = "security-group"
  vpc_id = aws_vpc.vpc.id
  ingress {
    protocol    = "tcp"
    from_port   = 80
    to_port     = 80
    cidr_blocks = ["0.0.0.0/0"]
  }
  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

# ################################################################################
# ECS - CLUSTER
# ################################################################################

resource "aws_ecs_cluster" "cluster" {
  name = var.ecs_cluster_name
  tags = merge({ Name = var.ecs_cluster_name }, var.tags)
}

resource "aws_ecs_cluster_capacity_providers" "cluster" {
  cluster_name       = aws_ecs_cluster.cluster.name
  capacity_providers = ["FARGATE"]
  default_capacity_provider_strategy {
    base              = 1
    weight            = 100
    capacity_provider = "FARGATE"
  }
}

# ################################################################################
# ECS - SERVICE
# ################################################################################

resource "aws_ecs_service" "api_ecs_service" {
  name            = var.api_service_name
  cluster         = aws_ecs_cluster.cluster.id
  task_definition = aws_ecs_task_definition.api_ecs_task.arn
  launch_type     = "FARGATE"
  desired_count   = 1
  load_balancer {
    target_group_arn = aws_lb_target_group.fsh_api_tg.arn
    container_name   = var.api_service_name
    container_port   = 80
  }
  network_configuration {
    subnets          = [aws_subnet.private_east_a.id, aws_subnet.private_east_b.id]
    security_groups  = [aws_security_group.lb.id]
    assign_public_ip = true
  }
  tags = merge({ Name = var.ecs_cluster_name }, var.tags)
}


resource "aws_cloudwatch_log_group" "api_log_group" {
  name              = "fsh/dotnet-webapi"
  retention_in_days = 1
}