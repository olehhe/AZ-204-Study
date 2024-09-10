# Provision a Cosmos DB account
az cosmosdb create \
    --name <cosmosdb-account-name> \
    --resource-group <resource-group-name> \
    --kind GlobalDocumentDB \
    --locations regionName=<region-name> failoverPriority=0 isZoneRedundant=False \
    --default-consistency-level Eventual

# List all Cosmos DB accounts in a resource group
az cosmosdb list \
    --resource-group <resource-group-name>

# Update the consistency policy of a Cosmos DB account
az cosmosdb update \
    --name <cosmosdb-account-name> \
    --resource-group <resource-group-name> \
    --default-consistency-level Strong

# Delete a Cosmos DB account
az cosmosdb delete \
    --name <cosmosdb-account-name> \
    --resource-group <resource-group-name>