## Stop Pods
```ps
kubectl scale deploy commands-depl --replicas=0
kubectl scale deploy platforms-depl --replicas=0
```

## Start Pods
```ps
kubectl scale deploy commands-depl --replicas=1
kubectl scale deploy platforms-depl --replicas=1
```

## Delete Ingress nginx
- Delete deployments
```ps
kubectl delete deploy ingress-nginx-controller -n ingress-nginx
```
- Delete pods
```ps
kubectl get pods -A
kubectl delete pods <id> -n ingress-nginx
```
- Delete services
```ps
kubectl get services -A
kubectl delete services <id> -n ingress-nginx
```
- Delete jobs
```ps
kubectl get jobs -A
kubectl delete jobs <id> -n ingress-nginx
```