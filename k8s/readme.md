## Configure ACR integration for existing AKS clusters
https://docs.microsoft.com/sv-se/azure/aks/cluster-container-registry-integration
az aks update -n okq8-ark-aks -g okq8-org-arkitektur --attach-acr okq8ark

## reload pod
kubectl rollout history deployment refark-customer -n refark
