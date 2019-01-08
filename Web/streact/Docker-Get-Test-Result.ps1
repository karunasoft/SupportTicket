param(
    [parameter(Mandatory=$true)][string]$theImage
)

Push-Location $PSScriptRoot

$testResultPath = "$PSScriptRoot/junit.xml"
$coverageFolderPath = "$PSScriptRoot/coverage"

if(Test-Path -path $testResultPath)
{
    Remove-Item -Force -Path $testResultPath
}

# run the image up as a detached container
Write-Host -Fore Yellow "Run up the image $theImage"
&docker run -dit $theImage /bin/bash

# get the last run container id
$containerId = docker ps -alq | % { $_.Split(" ")[0] } 
Write-Host "Last container id is $containerId"

# Copy the unit test result to host
&docker cp "$containerId`:/usr/src/app/test/junit.xml" $testResultPath

# Copy the code coverage directory to host
&docker cp "$containerId`:/usr/src/app/coverage/" $coverageFolderPath

# Stop and remove the container 
Write-Host -Fore Yellow "Stopping and removing $containerId"
&docker stop $containerId
&docker rm $containerId
Write-Host -Fore Yellow "done!"


if(Test-Path -path $testResultPath)
{
    Write-Host -Fore Yellow "Result copied to $testResultPath"
}
else
{
    Pop-Location
    throw "failed to get test result!"
}

Pop-Location

Write-Host -Fore Yellow "All done!"
