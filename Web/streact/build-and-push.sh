#!/bin/bash 
THE_VERSION=virasana/streact:1.0.0.$1
echo Building $THE_VERSION ...
docker build . -f ./Dockerfile -t virasana/streact:1.0.0.$1
echo Pushing $THE_VERSION ...
docker push $THE_VERSION
echo "done!"

