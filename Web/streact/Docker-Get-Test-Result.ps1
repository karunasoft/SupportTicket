param(
    [parameter(Mandatory=$true)][string]$theImage
)

Push-Location $PSScriptRoot

$thePath = "$PSScriptRoot/junit.xml"

if(Test-Path -path $thePath)
{
    Remove-Item -Force -Path $thePath
}

# run the image up as a detached container
Write-Host -Fore Yellow "Run up the image $theImage"
&docker run -dit $theImage /bin/bash

# get the last run container id
$containerId = docker ps -alq | % { $_.Split(" ")[0] } 
Write-Host "Last container id is $containerId"

# Copy the unit test result to host
&docker cp "$containerId`:/usr/src/app/test/junit.xml" $thePath

# Stop and remove the container 
Write-Host -Fore Yellow "Stopping and removing $containerId"
&docker stop $containerId
&docker rm $containerId
Write-Host -Fore Yellow "done!"


if(Test-Path -path $thePath)
{
    Write-Host -Fore Yellow "Result copied to $thePath"
}
else
{
    Pop-Location
    throw "failed to get test result!"
}

Pop-Location

Write-Host -Fore Yellow "All done!"
