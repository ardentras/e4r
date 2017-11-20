data "aws_vpc" "vpc" {
	filter = {
		name = "tag:Name"
		values = ["default"]
	}
}

data "aws_subnet_ids" "database" {
	# tags = {
	# 	"type" = "database"
	# }
	vpc_id = "${data.aws_vpc.vpc.id}"
}