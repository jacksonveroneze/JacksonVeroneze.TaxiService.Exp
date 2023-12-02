terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "5.19.0"
    }
  }

  backend "s3" {
    bucket  = "jacksonveroneze-tf-states"
    key     = "taxiservice-infra-ecs"
    region  = "sa-east-1"
    profile = "terraform"
  }
}

provider "aws" {
  region  = "sa-east-1"
  profile = "terraform"
}
