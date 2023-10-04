# ################################################################################
# SERVICE
# ################################################################################

resource "aws_apprunner_service" "apprunner_service" {
  service_name = "templatewebapi"

  observability_configuration {
    observability_configuration_arn = feito.observability_configuration.arn
    observability_enabled           = true
  }

  auto_scaling_configuration_arn = aws_apprunner_auto_scaling_configuration_version.auto_scaling_configuration_version.arn

  instance_configuration {
    cpu    = 256
    memory = 512
    //instance_role_arn = "arn:aws:iam::605060267998:role/service-role/AppRunnerECRAccessRole_Custom"
  }

  source_configuration {
    authentication_configuration {
      access_role_arn = "arn:aws:iam::605060267998:role/service-role/AppRunnerECRAccessRole_Custom"
    }

    image_repository {
      image_configuration {
        port = "8080"
        runtime_environment_variables = {
          "APP_CONFIG_Cache__Type" = "Memory",
          "APP_CONFIG_DistributedTracing__IsEnabled" : "false"
        }
      }
      image_identifier      = "605060267998.dkr.ecr.us-east-1.amazonaws.com/templatewebapi:latest"
      image_repository_type = "ECR"
    }
    auto_deployments_enabled = false
  }

  health_check_configuration {
    healthy_threshold   = 2
    unhealthy_threshold = 5
    interval            = 2
    path                = "/health?source=app-runner"
    protocol            = "HTTP"
    timeout             = 3
  }

  tags = merge({ Name = "apprunner-service" }, var.tags)
}

resource "aws_apprunner_observability_configuration" "observability_configuration" {
  observability_configuration_name = "apprunner-observability"

  trace_configuration {
    vendor = "AWSXRAY"
  }

  tags = merge({ Name = "apprunner-observability" }, var.tags)
}

resource "aws_apprunner_auto_scaling_configuration_version" "auto_scaling_configuration_version" {
  auto_scaling_configuration_name = "apprunner-autoscaling"

  max_concurrency = 50
  max_size        = 5
  min_size        = 1

  tags = merge({ Name = "apprunner-autoscaling" }, var.tags)
}
