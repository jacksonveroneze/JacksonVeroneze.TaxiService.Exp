terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "5.10.0"
    }
  }

  backend "s3" {
    bucket  = "templatewebapi"
    key     = "state-infra"
    region  = "us-east-1"
    profile = "terraform"
  }
}

provider "aws" {
  region  = "us-east-1"
  profile = "terraform"
}
