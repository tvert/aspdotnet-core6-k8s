docker build -f ".\catalog\Dockerfile" --force-rm -t catalog .
docker tag catalog jorndk8s.azurecr.io/catalog