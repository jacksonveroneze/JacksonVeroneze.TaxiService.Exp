################################################################################
# Common
################################################################################

variable "tags" {
  type = map(string)
  default = {
    terraform   = "true"
    environment = "dev"
    feature     = "common"
  }
}

variable "azs" {
  type    = list(string)
  default = ["sa-east-1a", "sa-east-1b"]
}

################################################################################
# VPC
################################################################################

variable "vpc_name" {
  type    = string
  default = "common-vpc"
}

variable "vpc_cidr_block" {
  type    = string
  default = "10.0.0.0/16"
}

variable "vpc_enable_dns_support" {
  type    = bool
  default = true
}

variable "vpc_enable_dns_hostnames" {
  type    = bool
  default = true
}

################################################################################
# IG
################################################################################

variable "internet_gateway_name" {
  type    = string
  default = "common-ig"
}

################################################################################
# SUBNETS - PUBLIC
################################################################################

variable "public_subnet_cidrs" {
  type    = list(string)
  default = ["10.0.1.0/24", "10.0.2.0/24"]
}

variable "public_subnet_name" {
  type    = string
  default = "common-pub-subnet"
}

################################################################################
# SUBNETS - PRIVATE
################################################################################

variable "private_subnet_cidrs" {
  type        = list(string)
  default     = ["10.0.3.0/24", "10.0.4.0/24"]
}

variable "private_subnet_name" {
  type    = string
  default = "common-pvt-subnet"
}

################################################################################
# ROUTE TABLE
################################################################################

variable "route_table_public_name" {
  type    = string
  default = "common-route-table-pub"
}

variable "route_table_private_name" {
  type    = string
  default = "common-route-table-pvt"
}