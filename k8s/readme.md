## Configure ACR integration for existing AKS clusters
https://docs.microsoft.com/sv-se/azure/aks/cluster-container-registry-integration
az aks update -n okq8-ark-aks -g okq8-org-arkitektur --attach-acr okq8ark

## deploy for the first time
kubectl apply -f refark-car-service.yaml
kubectl apply -f refark-car-ingress.yaml
kubectl apply -f refark-car-deployment.yaml


## reload pod
kubectl rollout restart deployment/refark-car -n refark

### list history
kubectl rollout history deployment refark-customer -n refark
