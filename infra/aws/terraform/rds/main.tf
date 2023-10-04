resource "aws_security_group" "security_group_rds" {
  vpc_id = data.aws_vpc.vpc_id.id
  ingress {
    from_port   = 5432
    to_port     = 5432
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }
  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

resource "aws_db_subnet_group" "db_subnet_group" {
  name       = "db_subnet_group"
  subnet_ids = var.subnets_ids
}

resource "aws_db_instance" "postgres" {
  allocated_storage      = 5
  db_name                = var.db_name
  engine                 = "postgres"
  engine_version         = "14.6"
  instance_class         = "db.t3.micro"
  username               = var.username
  password               = var.password
  identifier             = var.db_name
  vpc_security_group_ids = [aws_security_group.security_group_rds.id]
  db_subnet_group_name   = aws_db_subnet_group.db_subnet_group.name
  multi_az               = false
  publicly_accessible    = true
  skip_final_snapshot    = true
}


resource "aws_secretsmanager_secret" "database_username" {
  name = "/TemplateWebApi/Common/RDS/DatabaseUsername"
}

resource "aws_secretsmanager_secret_version" "database_username" {
  secret_id     = aws_secretsmanager_secret.database_username.id
  secret_string = var.username
}

##

resource "aws_secretsmanager_secret" "database_password" {
  name = "/TemplateWebApi/Common/RDS/DatabasePassword"
}

resource "aws_secretsmanager_secret_version" "database_password" {
  secret_id     = aws_secretsmanager_secret.database_password.id
  secret_string = var.password
}

##

resource "aws_secretsmanager_secret" "database_endpoint" {
  name = "/TemplateWebApi/Common/RDS/DatabaseEndpoint"
}

resource "aws_secretsmanager_secret_version" "database_endpoint" {
  secret_id     = aws_secretsmanager_secret.database_endpoint.id
  secret_string = aws_db_instance.postgres.endpoint
}
