################################################################################
# CLOUDWATCH
################################################################################

resource "aws_cloudwatch_log_group" "cloudwatch_log_group" {
  name              = "log-group-${var.feature_name}"
  retention_in_days = var.log_group_retention
}

# ################################################################################
# SECURITY GROUP
# ################################################################################

resource "aws_security_group" "security_group" {
  name   = "security-group-${var.feature_name}"
  vpc_id = data.aws_vpc.vpc_id.id
  ingress {
    protocol    = "tcp"
    from_port   = var.container_port
    to_port     = var.container_port
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
# ECS - TASK DEFINITION
# ################################################################################

resource "aws_ecs_task_definition" "ecs_task_definition" {
  family                   = "family-${var.feature_name}"
  cpu                      = var.task_cpu
  memory                   = var.task_memory
  network_mode             = "awsvpc"
  execution_role_arn       = data.aws_ssm_parameter.ssm_ecs_execution_role_arn.value
  task_role_arn            = data.aws_ssm_parameter.ssm_ecs_task_role_arn.value
  requires_compatibilities = ["FARGATE"]
  container_definitions    = jsonencode([
    {
      name              = var.task_container_name
      image             = var.task_container_image
      cpu               = var.task_cpu
      memory            = var.task_memory
      memoryReservation = var.task_memory_reservation
      essential         = true
      portMappings      = [
        {
          containerPort = var.container_port
        }
      ]
      logConfiguration = {
        logDriver = "awslogs"
        options   = {
          awslogs-region        = data.aws_region.current.name
          awslogs-group         = aws_cloudwatch_log_group.cloudwatch_log_group.name
          awslogs-stream-prefix = "log"
          mode                  = "non-blocking"
        }
      }
      environment = [
        {
          name  = "APP_CONFIG_Database__ConnectionString",
          value = "Host=${data.aws_secretsmanager_secret_version.database_endpoint.secret_string};Port=5432;Database=taxiservice;Username=${data.aws_secretsmanager_secret_version.database_username.secret_string};Password=${data.aws_secretsmanager_secret_version.database_password.secret_string}"
        }
      ]
      healthCheck = {
        command     = ["CMD-SHELL", "curl -f http://localhost:${var.container_port}/health || exit 1"]
        interval    = var.task_healthcheck_interval
        retries     = var.task_healthcheck_retries
        startPeriod = var.task_healthcheck_start_period
        timeout     = var.task_healthcheck_timeout
      }
    }
  ])
}

# ################################################################################
# ECS - CLUSTER
# ################################################################################

resource "aws_ecs_cluster" "ecs_cluster" {
  name = "cluster-${var.feature_name}"
  tags = var.tags
}

resource "aws_ecs_cluster_capacity_providers" "ecs_cluster_capacity_providers" {
  cluster_name       = aws_ecs_cluster.ecs_cluster.name
  capacity_providers = ["FARGATE_SPOT"]
  default_capacity_provider_strategy {
    base              = 1
    weight            = 100
    capacity_provider = "FARGATE_SPOT"
  }
}

# ################################################################################
# ECS - SERVICE
# ################################################################################

resource "aws_ecs_service" "ecs_service" {
  name    = "service-${var.feature_name}"
  cluster = aws_ecs_cluster.ecs_cluster.id
  deployment_circuit_breaker {
    enable   = true
    rollback = true
  }
  deployment_minimum_healthy_percent = 100
  deployment_maximum_percent         = 200
  desired_count                      = var.desired_number_of_tasks
  capacity_provider_strategy {
    base              = 1
    weight            = 100
    capacity_provider = "FARGATE_SPOT"
  }
  load_balancer {
    container_name   = var.task_container_name
    container_port   = var.container_port
    target_group_arn = aws_lb_target_group.lb_target_group.arn
  }
  network_configuration {
    assign_public_ip = true
    subnets          = data.aws_subnets.subnets_ids.ids
    security_groups  = [aws_security_group.security_group.id]
  }
  scheduling_strategy = "REPLICA"
  task_definition     = aws_ecs_task_definition.ecs_task_definition.arn
  tags                = var.tags
}

# ################################################################################
# NLB
# ################################################################################

resource "aws_lb_target_group" "lb_target_group" {
  name = "target-group-${var.feature_name}"
  port = var.container_port
  health_check {
    enabled             = true
    path                = "/health"
    protocol            = "HTTP"
    port                = var.container_port
    interval            = 5
    timeout             = 5
    healthy_threshold   = 2
    unhealthy_threshold = 2
  }
  protocol             = "TCP"
  vpc_id               = data.aws_vpc.vpc_id.id
  target_type          = "ip"
  deregistration_delay = 5
}

resource "aws_lb_listener" "lb_listener" {
  port              = var.container_port
  protocol          = "TCP"
  load_balancer_arn = var.load_balancer_arn
  default_action {
    target_group_arn = aws_lb_target_group.lb_target_group.arn
    type             = "forward"
  }
}

# ################################################################################
# AUTOSCALING
# ################################################################################

resource "aws_appautoscaling_target" "appautoscaling_target" {
  max_capacity       = var.max_number_of_tasks
  min_capacity       = var.min_number_of_tasks
  resource_id        = "service/${aws_ecs_cluster.ecs_cluster.name}/${aws_ecs_service.ecs_service.name}"
  scalable_dimension = "ecs:service:DesiredCount"
  service_namespace  = "ecs"
}

resource "aws_appautoscaling_policy" "appautoscaling_policy_cpu" {
  name               = "appautoscaling-policy-cpu"
  policy_type        = "TargetTrackingScaling"
  resource_id        = aws_appautoscaling_target.appautoscaling_target.resource_id
  scalable_dimension = aws_appautoscaling_target.appautoscaling_target.scalable_dimension
  service_namespace  = aws_appautoscaling_target.appautoscaling_target.service_namespace

  target_tracking_scaling_policy_configuration {
    predefined_metric_specification {
      predefined_metric_type = "ECSServiceAverageCPUUtilization"
    }

    target_value       = var.scaling_target_cpu
    scale_in_cooldown  = var.scaling_in_cool_down
    scale_out_cooldown = var.scaling_out_cool_down
  }
}

resource "aws_appautoscaling_policy" "appautoscaling_policy_memory" {
  name               = "appautoscaling-policy-memory"
  policy_type        = "TargetTrackingScaling"
  resource_id        = aws_appautoscaling_target.appautoscaling_target.resource_id
  scalable_dimension = aws_appautoscaling_target.appautoscaling_target.scalable_dimension
  service_namespace  = aws_appautoscaling_target.appautoscaling_target.service_namespace

  target_tracking_scaling_policy_configuration {
    predefined_metric_specification {
      predefined_metric_type = "ECSServiceAverageMemoryUtilization"
    }

    target_value       = var.scaling_target_memory
    scale_in_cooldown  = var.scaling_in_cool_down
    scale_out_cooldown = var.scaling_out_cool_down
  }
}