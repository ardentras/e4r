### Provider ###
# The AWS region
aws_region = "us-west-2"

### RDS ###
#Custom name of the DB instance (NOT a database name)
rds_instance_identifier = "e4rdb"
#List of CIDR netblocks for database security group, e.g. ["10.0.1.0/24", "10.0.2.0/24]
private_cidr = ["10.0.254.0/24", "10.0.253.0/24"]
#The number of GBs to allocate. Input must be an integer, e.g. 10
rds_allocated_storage = 20
#Instance size, eg. db.t2.micro
rds_instance_class = "db.t2.micro"
#Name of the dabatase
database_name = "e4rdb"
#User name (admin user)
database_user = "cooluser"
#Admin password - must be longer than 8 characters
database_password = "coolpassword"
#Subnets for database
subnets = []
#Database port (needed for a security group)
database_port = 1433
#Multi Availability Zone, set to true on production
rds_is_multi_az = false
#Public accessibility, set to false eventually
publicly_accessible = true