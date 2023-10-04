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
    name = "vpc-id"
    values = [ data.aws_vpc.vpc_id.id ]
  }

  filter {
    name = "tag:Name"
    values = ["common-pvt-subnet-*"]
  }
}