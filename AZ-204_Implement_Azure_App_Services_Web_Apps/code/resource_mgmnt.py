from azure.identity import DefaultAzureCredential
from azure.mgmt.web import WebSiteManagementClient

# Authenticate using default credentials
credential = DefaultAzureCredential()

# Create a WebSiteManagementClient
client = WebSiteManagementClient(credential, subscription_id)

# Define the resource group and app service name
resource_group = "my-resource-group"
app_service_name = "my-app-service"

# Create the app service
app_service = client.web_apps.create_or_update(
    resource_group,
    app_service_name,
    {
        "location": "westus",
        "server_farm_id": "/subscriptions/{subscription_id}/resourceGroups/{resource_group}/providers/Microsoft.Web/serverfarms/{server_farm_name}"
    }
)

print("App Service created successfully!")

# List app services
app_services = client.web_apps.list_by_resource_group(resource_group)
for app_service in app_services:
    print(app_service.name)

# Update app service
app_service = client.web_apps.get(resource_group, app_service_name)
app_service.https_only = True
app_service.tags = {"key1": "value1", "key2": "value2"}
client.web_apps.create_or_update(resource_group, app_service_name, app_service)
print("App Service updated successfully!")

# Delete app service
client.web_apps.delete(resource_group, app_service_name)
print("App Service deleted successfully!")
