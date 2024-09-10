#!/bin/bash

# Provision an Azure Function
az functionapp create \
    --name myfunctionapp \
    --resource-group myresourcegroup \
    --consumption-plan-location westeurope \
    --runtime node \
    --runtime-version 14 \
    --functions-version 3

# List all Azure Functions
az functionapp list \
    --resource-group myresourcegroup \
    --query "[].name" \
    --output tsv

# Update an Azure Function
az functionapp config appsettings set \
    --name myfunctionapp \
    --resource-group myresourcegroup \
    --settings MY_SETTING=123

# Delete an Azure Function
az functionapp delete \
    --name myfunctionapp \
    --resource-group myresourcegroup \
    --yes