terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "5.14.0"
    }
  }

  backend "s3" {
    bucket  = "jacksonveroneze-tf-states"
    key     = "templatewebapi"
    region  = "us-east-1"
    profile = "default"
  }
}

provider "aws" {
  region  = "us-east-1"
  profile = "default"
}
