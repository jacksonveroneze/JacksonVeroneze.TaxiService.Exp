# ################################################################################
# 1. VPC
# ################################################################################

resource "aws_vpc" "vpc" {
  cidr_block           = var.vpc_cidr_block
  enable_dns_support   = var.vpc_enable_dns_support
  enable_dns_hostnames = var.vpc_enable_dns_hostnames
  tags                 = merge({ Name = var.vpc_name }, var.tags)
}

# ################################################################################
# 2. IG
# ################################################################################

resource "aws_internet_gateway" "internet_gateway" {
  vpc_id = aws_vpc.vpc.id
  tags   = merge({ Name = var.internet_gateway_name }, var.tags)
}

# ################################################################################
# 3. PUBLIC SUBNET
# 4. PRIVATE SUBNET
# ################################################################################

resource "aws_subnet" "public_subnets" {
  count             = length(var.public_subnet_cidrs)
  vpc_id            = aws_vpc.vpc.id
  cidr_block        = element(var.public_subnet_cidrs, count.index)
  availability_zone = element(var.azs, count.index)
  tags              = merge({ Name = "${var.public_subnet_name}-${element(var.azs, count.index)}" }, var.tags)
}

resource "aws_subnet" "private_subnets" {
  count             = length(var.private_subnet_cidrs)
  vpc_id            = aws_vpc.vpc.id
  cidr_block        = element(var.private_subnet_cidrs, count.index)
  availability_zone = element(var.azs, count.index)
  tags              = merge({ Name = "${var.private_subnet_name}-${element(var.azs, count.index)}" }, var.tags)
}

# ################################################################################
# 5. ROUTE TABLE
# ################################################################################

resource "aws_route_table" "route_table_public" {
  vpc_id = aws_vpc.vpc.id

  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.internet_gateway.id
  }

  tags = merge({ Name = var.route_table_public_name }, var.tags)
}

# ################################################################################
# 6. ROUTE TABLE - ASSOCIATION
# ################################################################################

resource "aws_route_table_association" "route_table_association_public_subnet" {
  count          = length(var.public_subnet_cidrs)
  subnet_id      = element(aws_subnet.public_subnets[*].id, count.index)
  route_table_id = aws_route_table.route_table_public.id
}
