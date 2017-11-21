provider "aws" {
	access_key = "AKIAJPNSGLPRCWGNSTQQ"
	secret_key = "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"
	region = "${var.aws_region}"
	version = "~> 0.1"
}

provider "terraform" {
	version = "~> 0.1"
}

module "rds" {
	source = "../modules/RDS"

	#Custom name of the DB instance (NOT a database name)
	rds_instance_identifier = "${var.rds_instance_identifier}"
	#List of CIDR netblocks for database security group, e.g. ["10.0.1.0/24", "10.0.2.0/24]
	private_cidr = "${var.private_cidr}"
	#The number of GBs to allocate. Input must be an integer, e.g. 10
	rds_allocated_storage = "${var.rds_allocated_storage}"
	#Instance size, eg. db.t2.micro
	rds_instance_class = "${var.rds_instance_class}"
	#Name of the dabatase
	database_name = "${var.database_name}"
	#User name (admin user)
	database_user = "${var.database_user}"
	#Admin password - must be longer than 8 characters
	database_password = "${var.database_password}"
	#List of subnets IDs in a list form, e.g. ["sb-1234567890", "sb-0987654321"]
	subnets = "${data.aws_subnet_ids.database.ids}"
	#Database port (needed for a security group)
	database_port = "${var.database_port}"
	#VPC ID DB will be connected to
	rds_vpc_id = "${data.aws_vpc.vpc.id}"
	#Multi Availability Zone, set to true on production
	rds_is_multi_az = "${var.rds_is_multi_az}"

	publicly_accessible = "${var.publicly_accessible}"
}