docker build -f ".\ordering\Dockerfile" --force-rm -t ordering .
docker tag ordering jorndk8s.azurecr.io/ordering