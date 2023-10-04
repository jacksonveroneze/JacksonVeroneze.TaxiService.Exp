################################################################################
# COMMON
################################################################################

variable "tags" {
  type = map(string)
  default = {
    terraform   = "true"
    environment = "dev"
    feature     = "templatewebapi"
  }
}

variable "feature_name" {
  type    = string
  default = "templatewebapi"
}

################################################################################
# CLOUDWATCH
################################################################################

variable "log_group_name" {
  type    = string
  default = "templatewebapi"
}

variable "log_group_retention" {
  type    = number
  default = 1
}

################################################################################
# VPC
################################################################################

variable "vpc_id" {
  type    = string
  default = "vpc-0e296d7e4288511c0"
}

################################################################################
# SUBNETS
################################################################################

variable "subnets_ids" {
  type    = list(string)
  default = ["subnet-078f6edc126a61a46", "subnet-0bcc5a8bc29918156"]
}

# ################################################################################
# ECS - CLUSTER
# ################################################################################

variable "ecs_cluster_name" {
  type    = string
  default = "cluster-templatewebapi"
}

# ################################################################################
# ECS - SERVICE
# ################################################################################

variable "ecs_service_name" {
  type    = string
  default = "service-templatewebapi"
}

# ################################################################################
# ECS - TASK DEFINITION
# ################################################################################

variable "task_container_name" {
  type    = string
  default = "api"
}

variable "task_container_image" {
  type    = string
  default = "jacksonveroneze/templatewebapi-service:0.0.5"
}

variable "task_cpu" {
  type    = number
  default = 256
}

variable "task_memory" {
  type    = number
  default = 512
}

variable "task_memory_reservation" {
  type    = number
  default = 512
}

variable "container_port" {
  type    = number
  default = 8080
}

variable "task_healthcheck_interval" {
  type    = number
  default = 15
}

variable "task_healthcheck_retries" {
  type    = number
  default = 3
}

variable "task_healthcheck_start_period" {
  type    = number
  default = 5
}

variable "task_healthcheck_timeout" {
  type    = number
  default = 5
}

# ################################################################################
# LOAD BALANCER
# ################################################################################

variable "load_balancer_name" {
  type    = string
  default = "nlb-templatewebapi"
}

variable "target_group_name" {
  type    = string
  default = "target-group-templatewebapi"
}
