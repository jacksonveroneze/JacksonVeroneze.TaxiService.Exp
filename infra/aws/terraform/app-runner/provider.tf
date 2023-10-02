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
    region  = "sa-east-1"
    profile = "default"
  }
}

provider "aws" {
  region  = "sa-east-1"
  profile = "terraform"
}
