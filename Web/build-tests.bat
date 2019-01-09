SET THE_VERSION=virasana/stweb:1.0.0.0 @REM %1

echo Building %THE_VERSION% ...
docker build . -f .\ST.Web\Dockerfile --target test -t virasana/stweb:1.0.0.0 --no-cache
