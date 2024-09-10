# Provision an Azure Redis Cache
az redis create \
    --name <cache-name> \
    --resource-group <resource-group-name> \
    --location <location> \
    --sku <sku> \
    --vm-size <vm-size> \
    --enable-non-ssl-port <true/false>

# Update an Azure Redis Cache
az redis update \
    --name <cache-name> \
    --resource-group <resource-group-name> \
    --sku <new-sku> \
    --vm-size <new-vm-size>

# Delete an Azure Redis Cache
az redis delete \
    --name <cache-name> \
    --resource-group <resource-group-name>