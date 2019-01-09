param(
    [parameter(Mandatory=$true)][string]$theImage
)

Push-Location $PSScriptRoot

$testResultPath = "/TestResults"
$destPath = "./TestResults"


if(Test-Path -path $destPath)
{
    Write-Host -Fore Yellow "Remove $destPath"
    Remove-Item -Force -Path $destPath -Recurse
}

Write-Host -Fore Yellow "Creating container named 'extract'"
docker container create --name extract $theImage

Write-Host -Fore Yellow "Extract $testResultPath folder (in container) to local path $destPath"
docker container cp extract:/TestResults $destPath  

Write-Host -Fore Yellow "Remove container 'extract'"
docker container rm -f extract

Write-Host -Fore Yellow "done!"


if(Test-Path -path $destPath)
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
