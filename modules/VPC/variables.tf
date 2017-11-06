variable "name" {
  description = "Name to be used on all the resources as identifier"
}

variable "cidr" {
  description = "The CIDR block for the VPC"
}

variable "public_subnets" {
  type        = "list"
  description = "A list of public subnets inside the VPC."
}

variable "private_subnets" {
  type        = "list"
  description = "A list of private subnets inside the VPC."
}

variable "envoy_database_subnets" {
  type        = "list"
  description = "A list of database subnets"
}

variable "cloud_database_subnets" {
  type        = "list"
  description = "A list of database subnets"
}

variable "azs" {
  type        = "list"
  description = "A list of Availability zones in the region"
}

variable "enable_dns_hostnames" {
  description = "should be true if you want to use private DNS within the VPC"
}

variable "enable_dns_support" {
  description = "should be true if you want to use private DNS within the VPC"
}

variable "enable_nat_gateway" {
  description = "should be true if you want to provision NAT Gateways for each of your private networks"
}

variable "map_public_ip_on_launch" {
  description = "should be false if you do not want to auto-assign public IP on launch"
}

variable "tags" {
  type = "map"
  description = "A map of tags to add to all resources"
}

variable "public_subnet_tags" {
  type = "map"
  description = "Additional tags for the public subnets"
}

variable "private_subnet_tags" {
  type = "map"
  description = "Additional tags for the public subnets"
}

variable "envoy_database_subnet_tags" {
  type = "map"
  description = "Additional tags for the database subnets"
}

variable "cloud_database_subnet_tags" {
  type = "map"
  description = "Additional tags for the database subnets"
}

variable "vpn_cidr" {
  description = "CIDR block for the VPN EC2 instance"
}

variable "vpn_instance_id" {
  description = "The instance ID of the VPN EC2 instance"
}