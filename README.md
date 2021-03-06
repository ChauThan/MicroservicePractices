# Microservice Practices.
Les Jackson has introduced a new course for Microservice on Youtube.  
This repo contains my works of his course. 
Give him a subscribe and a like if you interested with this course.  
[Microservice Course](https://www.youtube.com/watch?v=DgVjEo3OGBI)

## Prerequisite:
- VSCode or Visual Studio
- .NET Core SDK 5
- Docker
- Kubernetes

## Build Docker image
Before start containers, we have to create docker images for our services.
- Open terminal and set current directory to platform folder.
```ps
docker build -t chauthan\platformservice .
```
- Then go to command folder.
```ps
docker build -t chauthan\commandservice .
```

## Deploy to K8S
In source folder, apply all yaml files.
```ps
kubectl apply -f .\K8S\
```
These resources are deployed under `microservice-practice` namespace. You can use below command to remove all resources.

```ps
kubectl delete pvc,svc,secret,deploy --all -n microservice-practice
```