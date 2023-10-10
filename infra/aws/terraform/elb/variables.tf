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
