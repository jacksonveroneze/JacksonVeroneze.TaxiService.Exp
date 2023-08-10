################################################################################
# Common
################################################################################

variable "tags" {
  type    = map(string)
  default = {
    terraform   = "true"
    environment = "dev"
    feature     = "templatewebapi"
  }
}

variable "email_alarm" {
  type    = string
  default = "jackson@jacksonveroneze.com"
}

################################################################################
# VPC
################################################################################

variable "vpc_name" {
  type    = string
  default = "vpc-templatewebapi"
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
  default = "ig-templatewebapi"
}

################################################################################
# SUBNET - PUBLIC
################################################################################

variable "subnet_public_1_name" {
  type    = string
  default = "subnet-templatewebapi-public-1"
}

variable "subnet_public_1_cidr_block" {
  type    = string
  default = "10.0.1.0/24"
}

variable "subnet_public_1_az" {
  type    = string
  default = "us-east-1a"
}

#############################################

variable "subnet_public_2_name" {
  type    = string
  default = "subnet-templatewebapi-public-2"
}

variable "subnet_public_2_cidr_block" {
  type    = string
  default = "10.0.2.0/24"
}

variable "subnet_public_2_az" {
  type    = string
  default = "us-east-1b"
}

################################################################################
# SUBNET - PRIVATE
################################################################################

variable "subnet_private_1_name" {
  type    = string
  default = "subnet-templatewebapi-private-1"
}

variable "subnet_private_1_cidr_block" {
  type    = string
  default = "10.0.3.0/24"
}

variable "subnet_private_1_az" {
  type    = string
  default = "us-east-1a"
}

#############################################

variable "subnet_private_2_name" {
  type    = string
  default = "subnet-templatewebapi-private-2"
}

variable "subnet_private_2_cidr_block" {
  type    = string
  default = "10.0.4.0/24"
}

variable "subnet_private_2_az" {
  type    = string
  default = "us-east-1b"
}