#!/bin/sh
# Dot not forget -> chmod a+rx docker-build-catalog.sh
docker build -f catalog/Dockerfile --force-rm -t catalog .
docker tag catalog jorndk8s.azurecr.io/catalog
echo 'Ready to push -> docker push jorndk8s.azurecr.io/catalog'