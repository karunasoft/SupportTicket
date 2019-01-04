# These commands will help you to create the RDS database with appropriate security groups so that the sqlservice can access the database
# Need to execute this one manually as you need the Security Group Id.  TODO - grep it
export AWS_REGION=eu-west-1
export RDS_DATABASE_NAME=kssupportticketsql
export RDS_USER_NAME=addy
export RDS_PASSWORD=#password#

aws ec2 create-security-group --description kssupportticketdb --group-name kssupportticketdb --region eu-west-1a

# Get the security group id from the output of the above
export SECURITY_GROUP_ID=#sg-03bae1f62d6e85fb7#

# Be careful - the following grants access to all
aws ec2 authorize-security-group-ingress --group-id ${SECURITY_GROUP_ID} --protocol tcp --port 1433 --cidr 0.0.0.0/0 --region eu-west-1a

aws rds create-db-instance --db-instance-identifier ${RDS_DATABASE_NAME} --vpc-security-group-ids ${SECURITY_GROUP_ID} /
	--allocated-storage 20 --db-instance-class db.t2.micro --engine sqlserver-ex --master-username ${RDS_USER_NAME} --master-user-password ${RDS_PASSWORD} /
	--region eu-west-1a
	
	
# Get the endpoint address e.g. kssupportticketsql.c7qfid0xqzke.eu-west-1.rds.amazonaws.com  look in the output for Endpoint: ...
aws rds describe-db-instances --db-instance-identifier ${RDS_DATABASE_NAME} --region ${AWS_REGION}

	