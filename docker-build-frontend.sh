#!/bin/sh
# Dot not forget -> chmod a+rx docker-build-frontend.sh
docker build -f frontend/Dockerfile --force-rm -t frontend .
docker tag frontend jorndk8s.azurecr.io/frontend
echo 'Ready to push -> docker push jorndk8s.azurecr.io/frontend'