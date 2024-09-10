# Provision an Azure Blob Service
az storage account create \
    --name <storage_account_name> \
    --resource-group <resource_group_name> \
    --location <location> \
    --sku Standard_LRS \
    --kind StorageV2 \
    --access-tier Hot

# Update the properties of an Azure Blob Service
az storage account update \
    --name <storage_account_name> \
    --resource-group <resource_group_name> \
    --default-action Allow

# Delete an Azure Blob Service
az storage account delete \
    --name <storage_account_name> \
    --resource-group <resource_group_name> \
    --yes