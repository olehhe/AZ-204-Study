#!/bin/bash

# Set your Azure subscription ID
subscriptionId="<your-subscription-id>"

# Set your resource group name
resourceGroupName="<your-resource-group-name>"

# Set your storage account name
storageAccountName="<your-storage-account-name>"

# Set your container name
containerName="<your-container-name>"

# Set your blob name
blobName="<your-blob-name>"

# Authenticate with Azure CLI
az login

# Set the active subscription
az account set --subscription $subscriptionId

# Create a new storage account
az storage account create \
    --name $storageAccountName \
    --resource-group $resourceGroupName \
    --location <your-location> \
    --sku Standard_LRS

# Create a new container in the storage account
az storage container create \
    --name $containerName \
    --account-name $storageAccountName \
    --account-key <your-storage-account-key>

# Upload a blob to the container
az storage blob upload \
    --account-name $storageAccountName \
    --account-key <your-storage-account-key> \
    --container-name $containerName \
    --name $blobName \
    --type block \
    --file <path-to-local-file>

# Download a blob from the container
az storage blob download \
    --account-name $storageAccountName \
    --account-key <your-storage-account-key> \
    --container-name $containerName \
    --name $blobName \
    --file <path-to-local-file>

# List blobs in the container
az storage blob list \
    --account-name $storageAccountName \
    --account-key <your-storage-account-key> \
    --container-name $containerName