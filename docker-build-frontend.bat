docker build -f ".\frontend\Dockerfile" --force-rm -t frontend .
docker tag frontend jorndk8s.azurecr.io/frontend