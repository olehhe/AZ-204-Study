from azure.storage.blob import BlobServiceClient

# Connect to the storage account
connection_string = "<your_connection_string>"
blob_service_client = BlobServiceClient.from_connection_string(connection_string)

# Create a container
container_name = "<your_container_name>"
container_client = blob_service_client.create_container(container_name)

# Upload a blob to the container
blob_name = "<your_blob_name>"
blob_client = container_client.get_blob_client(blob_name)
with open("<path_to_local_file>", "rb") as data:
    blob_client.upload_blob(data)

# Download a blob from the container
downloaded_blob = blob_client.download_blob()
with open("<path_to_save_file>", "wb") as file:
    file.write(downloaded_blob.readall())

# List all blobs in the container
blobs = container_client.list_blobs()
for blob in blobs:
    print(blob.name)

# Delete a blob
blob_client.delete_blob()

# Delete the container
container_client.delete_container()