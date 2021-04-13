## Configure ACR integration for existing AKS clusters
https://docs.microsoft.com/sv-se/azure/aks/cluster-container-registry-integration
az aks update -n okq8-ark-aks -g okq8-org-arkitektur --attach-acr okq8ark

## Set Subscription OKQ8 Enabling Foundation
az account set --subscription 5f630daa-3230-4e1f-a83d-7ee00c9801c0

## Stop and Start AKS
az aks stop -g okq8-org-arkitektur -n okq8-ark-aks
az aks start -g okq8-org-arkitektur -n okq8-ark-aks

## deploy for the first time
kubectl apply -f refark-car-service.yaml
kubectl apply -f refark-car-ingress.yaml
kubectl apply -f refark-car-deployment.yaml


## reload pod
kubectl rollout restart deployment/refark-car -n refark

### list history
kubectl rollout history deployment refark-customer -n refark
