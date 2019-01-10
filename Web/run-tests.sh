#!/bin/bash

# this script runs inside the container

function RunTests {
	echo "======================================================"
	
	printf "=T=T=T=> Running Tests on %s \n" $1
	
	dotnet test $1 /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --logger "trx;LogFileName=$1results.trx" --results-directory "/TestResults" -c Release
	
	dir = $1
	dir=${dir%*/}
	dir=${dir##*/} 
	cp $1/coverage.cobertura.xml /TestResults/$1/coverage.cobertura.$dir.xml
	/root/.dotnet/tools/reportgenerator "-reports:/src/$1/coverage.cobertura.xml" "-targetdir:/TestResults/$1/CoverageReport" "-tag:CodeCoverage" "-reportTypes:htmlInline"
}

for dir in ./*/
do
	if [[ "$dir" == *\.Tests\/ ]]
	then
		RunTests $dir
	fi
done