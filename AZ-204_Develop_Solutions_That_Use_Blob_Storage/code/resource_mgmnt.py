from azure.storage.blob import BlobServiceClient

# Provision a storage account
connection_string = "<your_connection_string>"
storage_account_name = "<your_storage_account_name>"
container_name = "<your_container_name>"

blob_service_client = BlobServiceClient.from_connection_string(connection_string)

# Create a container
container_client = blob_service_client.create_container(container_name)

# List all containers
containers = blob_service_client.list_containers()
for container in containers:
    print(container.name)

# Update container properties
container_properties = container_client.get_container_properties()
container_properties.metadata = {"key": "value"}
container_client.set_container_metadata(container_properties)

# Delete the container
container_client.delete_container()