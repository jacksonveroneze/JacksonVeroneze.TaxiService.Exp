# ################################################################################
# VPC
# ################################################################################

resource "aws_vpc" "vpc" {
  cidr_block           = var.vpc_cidr_block
  enable_dns_support   = var.vpc_enable_dns_support
  enable_dns_hostnames = var.vpc_enable_dns_hostnames
  tags                 = merge({ Name = var.vpc_name }, var.tags)
}

# ################################################################################
# IG
# ################################################################################

resource "aws_internet_gateway" "internet_gateway" {
  vpc_id = aws_vpc.vpc.id
  tags   = merge({ Name = var.internet_gateway_name }, var.tags)
}

# ################################################################################
# SUBNET
# ################################################################################

resource "aws_subnet" "subnet_public_1" {
  vpc_id                  = aws_vpc.vpc.id
  cidr_block              = var.subnet_public_1_cidr_block
  availability_zone       = var.subnet_public_1_az
  map_public_ip_on_launch = true
  tags                    = merge({ Name = var.subnet_public_1_name }, var.tags)
}

resource "aws_subnet" "subnet_public_2" {
  vpc_id                  = aws_vpc.vpc.id
  cidr_block              = var.subnet_public_2_cidr_block
  availability_zone       = var.subnet_public_2_az
  map_public_ip_on_launch = true
  tags                    = merge({ Name = var.subnet_public_2_name }, var.tags)
}

resource "aws_subnet" "subnet_private_1" {
  vpc_id            = aws_vpc.vpc.id
  cidr_block        = var.subnet_private_1_cidr_block
  availability_zone = var.subnet_private_1_az
  tags              = merge({ Name = var.subnet_private_1_name }, var.tags)
}

resource "aws_subnet" "subnet_private_2" {
  vpc_id            = aws_vpc.vpc.id
  cidr_block        = var.subnet_private_2_cidr_block
  availability_zone = var.subnet_private_2_az
  tags              = merge({ Name = var.subnet_private_1_name }, var.tags)
}
