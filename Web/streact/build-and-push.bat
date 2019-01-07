SET THE_VERSION=virasana/streact:1.0.0.%1
echo Building %THE_VERSION% ...
docker build . -f ./Dockerfile-Run-Tests -t virasana/streact:1.0.0.%1 --no-cache
powershell .\Docker-Get-Test-Result.ps1 -theImage virasana/streact:1.0.0.%1
docker build . -f ./Dockerfile-Build-Prod -t virasana/streact:1.0.0.%1 --no-cache --build-arg base_image_name=%THE_VERSION%
@REM echo Pushing $THE_VERSION ...
@REM docker push $THE_VERSION
echo "done!"

