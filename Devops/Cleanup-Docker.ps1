# Prompts you to delete containers - press "n" to skip or "y" to delete

&docker ps -a | % {
    Write-Host -Fore Green $_
    $x = Read-Host
    if($x -eq "y")
    {
     $containerId = $_.Split(" ")[0]
     Write-Host -fore Yellow "deleting $containerId"
     &docker stop $containerId
     &docker rm $containerId
     Write-Host -fore Yellow "done!"     
    }
    else
    {
        Write-Host -Fore Yellow "Skipping..."
    }
}