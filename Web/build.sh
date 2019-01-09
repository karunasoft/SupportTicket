#!/bin/bash

# optionally this script runs inside the container 
# not really being used at the moment
# see Dockerfile for how we build

function CleanAndBuild {
	if [[ $1 == \.\/ST* ]];
	then
		echo "======================================================"

		printf "=C=C=C=> Cleaning %s \n" $1		
		dotnet clean $1
		
		printf "=B=B=B=> Building %s \n" $1
		dotnet build -c Release $1 -o app
	fi               
}

for dir in ./*/
do
	if [[ "$dir" == \.\/ST\.* ]]
	then
		CleanAndBuild $dir
	fi
done