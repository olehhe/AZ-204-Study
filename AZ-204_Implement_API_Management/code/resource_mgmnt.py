from azure.identity import DefaultAzureCredential
from azure.mgmt.apimanagement import ApiManagementClient

# Set your Azure subscription ID and resource group name
subscription_id = "<your-subscription-id>"
resource_group_name = "<your-resource-group-name>"

# Set your Azure credentials
credential = DefaultAzureCredential()

# Create the APIM client
apim_client = ApiManagementClient(credential, subscription_id)

# Provision an APIM resource
def provision_apim_resource(resource_name):
    apim_client.api_management.create_or_update(
        resource_group_name,
        resource_name,
        {
            "location": "<your-location>",
            "sku": {
                "name": "Developer",
                "capacity": 1
            }
        }
    )
    print("APIM resource provisioned successfully.")

# List all APIM resources
def list_apim_resources():
    resources = apim_client.api_management.list_by_resource_group(resource_group_name)
    for resource in resources:
        print(resource.name)

# Update an APIM resource
def update_apim_resource(resource_name, new_location):
    apim_client.api_management.update(
        resource_group_name,
        resource_name,
        {
            "location": new_location
        }
    )
    print("APIM resource updated successfully.")

# Delete an APIM resource
def delete_apim_resource(resource_name):
    apim_client.api_management.delete(resource_group_name, resource_name)
    print("APIM resource deleted successfully.")

# Usage examples
provision_apim_resource("<your-apim-resource-name>")
list_apim_resources()
update_apim_resource("<your-apim-resource-name>", "<new-location>")
delete_apim_resource("<your-apim-resource-name>")