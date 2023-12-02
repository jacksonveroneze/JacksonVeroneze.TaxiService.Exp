data "aws_region" "current" {}

data "aws_caller_identity" "current" {}

data "aws_vpc" "vpc_id" {
  filter {
    name   = "tag:Name"
    values = ["common-vpc"]
  }
}

data "aws_subnets" "subnets_ids" {
  filter {
    name   = "vpc-id"
    values = [data.aws_vpc.vpc_id.id]
  }

  filter {
    name   = "tag:Name"
    values = ["common-pvt-subnet-*"]
  }
}

data "aws_ssm_parameter" "ssm_ecs_execution_role_arn" {
  name = "/Infra/Common/ECS/DefaultECSTaskExecutionRoleArn"
}

data "aws_ssm_parameter" "ssm_ecs_task_role_arn" {
  name = "/Infra/Common/ECS/DefaultECSTaskRoleArn"
}

#####


data "aws_secretsmanager_secret" "database_username" {
  name = "/TaxiService/Common/RDS/DatabaseUsername"
}

data "aws_secretsmanager_secret_version" "database_username" {
  secret_id = data.aws_secretsmanager_secret.database_username.id
}

##

data "aws_secretsmanager_secret" "database_password" {
  name = "/TaxiService/Common/RDS/DatabasePassword"
}

data "aws_secretsmanager_secret_version" "database_password" {
  secret_id = data.aws_secretsmanager_secret.database_password.id
}

##

data "aws_secretsmanager_secret" "database_endpoint" {
  name = "/TaxiService/Common/RDS/DatabaseEndpoint"
}

data "aws_secretsmanager_secret_version" "database_endpoint" {
  secret_id = data.aws_secretsmanager_secret.database_endpoint.id
}
