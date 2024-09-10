# Provision an APIM resource
az apim create --name <apim-name> --resource-group <resource-group-name> --location <location>

# List all APIM resources
az apim list --resource-group <resource-group-name>

# Update an APIM resource
az apim update --name <apim-name> --resource-group <resource-group-name> --set <property-name>=<new-value>

# Delete an APIM resource
az apim delete --name <apim-name> --resource-group <resource-group-name>

# Create an API
az apim api create --api-id <api-id> --display-name <display-name> --service-name <apim-name> --resource-group <resource-group-name> --path <api-path> --protocols <api-protocols> --subscription <subscription-id>

# Create another API
az apim api create --api-id <api-id> --display-name <display-name> --service-name <apim-name> --resource-group <resource-group-name> --path <api-path> --protocols <api-protocols> --subscription <subscription-id>

# List all APIs in an APIM resource
az apim api list --service-name <apim-name> --resource-group <resource-group-name>

# Update an API
az apim api update --api-id <api-id> --service-name <apim-name> --resource-group <resource-group-name> --set <property-name>=<new-value>

# Delete an API
az apim api delete --api-id <api-id> --service-name <apim-name> --resource-group <resource-group-name>

# Add a policy to an API
az apim api policy create --api-id <api-id> --service-name <apim-name> --resource-group <resource-group-name> --xml "<policies><inbound><base /></inbound></policies>"

# Update a policy of an API
az apim api policy update --api-id <api-id> --service-name <apim-name> --resource-group <resource-group-name> --xml "<policies><inbound><base /></inbound></policies>"