#!/bin/bash

# Set variables
resourceGroupName="<resource-group-name>"
appName="<app-service-name>"
runtime="<runtime>"
sku="<sku>"

# Create resource group
az group create --name $resourceGroupName --location "<location>"

# Create App Service
az webapp create --name $appName --resource-group $resourceGroupName --runtime $runtime --sku $sku

# Display the App Service URL
az webapp show --name $appName --resource-group $resourceGroupName --query "defaultHostName" --output tsv

# List App Services
az webapp list --resource-group $resourceGroupName --query "[].name" --output tsv

# Update App Service
az webapp update --name $appName --resource-group $resourceGroupName --set httpsonly=true --add tags.Environment="Production" tags.Department="IT"

# Delete App Service
az webapp delete --name $appName --resource-group $resourceGroupName --yes