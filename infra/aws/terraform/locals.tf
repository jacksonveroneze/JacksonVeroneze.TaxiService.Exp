locals {
  common_tags = {
    environment = var.environment
    Owner       = var.owner
    Project     = var.project_name
  }
}