data "aws_region" "current" {}

data "aws_caller_identity" "current" {}

data "aws_ssm_parameter" "ssm_ecs_execution_role_arn" {
  name = "/Infra/Common/ECS/DefaultECSTaskExecutionRoleArn"
}

data "aws_ssm_parameter" "ssm_ecs_task_role_arn" {
  name = "/Infra/Common/ECS/DefaultECSTaskRoleArn"
}