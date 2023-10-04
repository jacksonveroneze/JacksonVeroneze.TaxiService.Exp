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
  default = "vpc-0000f5a7217465766"
}

################################################################################
# SUBNETS
################################################################################

variable "subnets_ids" {
  type    = list(string)
  default = ["subnet-0a4673c36225e7265", "subnet-01973a725a7679018"]
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
# AUTOSCALING
# ################################################################################

variable "desired_number_of_tasks" {
  type    = number
  default = 1
}

variable "min_number_of_tasks" {
  type    = number
  default = 1
}

variable "max_number_of_tasks" {
  type    = number
  default = 2
}

variable "scaling_target_cpu" {
  type    = number
  default = 50
}

variable "scaling_target_memory" {
  type    = number
  default = 70
}

variable "scaling_in_cool_down" {
  type    = number
  default = 60
}

variable "scaling_out_cool_down" {
  type    = number
  default = 60
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