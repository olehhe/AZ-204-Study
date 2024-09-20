#!/bin/bash

# Variables
RESOURCE_GROUP="myResourceGroup"
ACR_NAME="myContainerRegistry"
ACI_NAME="myContainerInstance"
ACA_NAME="myContainerApp"
IMAGE_NAME="$ACR_NAME.azurecr.io/myapp:latest"

# Create a resource group
az group create --name $RESOURCE_GROUP --location eastus

# Create an Azure Container Registry
az acr create --resource-group $RESOURCE_GROUP --name $ACR_NAME --sku Basic

# Log in to the ACR
az acr login --name $ACR_NAME

# Build and push a Docker image to ACR
az acr build --registry $ACR_NAME --image $IMAGE_NAME .

# Create an Azure Container Instance
az container create --resource-group $RESOURCE_GROUP --name $ACI_NAME --image $IMAGE_NAME --cpu 1 --memory 1.5 --restart-policy OnFailure

# Create an Azure Container App
az containerapp create --name $ACA_NAME --resource-group $RESOURCE_GROUP --image $IMAGE_NAME --target-port 80 --environment myContainerAppEnv

echo "Resources created successfully!"