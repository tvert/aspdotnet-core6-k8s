#!/bin/sh
# Dot not forget -> chmod a+rx docker-build-ordering.sh
docker build -f ordering/Dockerfile --force-rm -t ordering .
docker tag ordering jorndk8s.azurecr.io/ordering
echo 'Ready to push -> docker push jorndk8s.azurecr.io/ordering'