# ################################################################################
# SECURITY GROUP
# ################################################################################

resource "aws_security_group" "security_group" {
  name   = "security-group-nlb-8080"
  vpc_id = data.aws_vpc.vpc_id.id
  ingress {
    protocol    = "tcp"
    from_port   = 8080
    to_port     = 8080
    cidr_blocks = ["0.0.0.0/0"]
  }
  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

# ################################################################################
# NLB
# ################################################################################

resource "aws_lb" "nlb" {
  name               = "nlb-common"
  load_balancer_type = "network"
  security_groups    = [aws_security_group.security_group.id]
  internal           = false
  subnets            = data.aws_subnets.subnets_ids.ids
}