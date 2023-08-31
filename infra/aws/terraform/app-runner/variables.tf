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