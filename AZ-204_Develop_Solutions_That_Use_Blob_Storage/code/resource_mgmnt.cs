using Azure.Identity;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Threading.Tasks;

namespace BlobManagement
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string subscriptionId = "<your_subscription_id>";
            string resourceGroupName = "<your_resource_group_name>";
            string location = "<your_location>";
            string storageAccountName = "<your_storage_account_name>";
            string connectionString = "<your_connection_string>";
            string containerName = "<your_container_name>";
            string blobName = "<your_blob_name>";

            // Provision a new storage account
            var resourceClient = new ResourcesManagementClient(subscriptionId, new DefaultAzureCredential());
            var storageClient = new StorageManagementClient(subscriptionId, new DefaultAzureCredential());

            var resourceGroup = await resourceClient.ResourceGroups.CreateOrUpdateAsync(resourceGroupName, new ResourceGroup(location));
            var storageAccount = await storageClient.StorageAccounts.CreateOrUpdateAsync(resourceGroupName, storageAccountName, new StorageAccountCreateParameters(SkuName.Standard_LRS, Kind.StorageV2, location));

            // Get the connection string for the storage account
            var keys = await storageClient.StorageAccounts.ListKeysAsync(resourceGroupName, storageAccountName);
            connectionString = $"DefaultEndpointsProtocol=https;AccountName={storageAccountName};AccountKey={keys.Keys[0].Value};EndpointSuffix=core.windows.net";

            // Provision a new Blob Service
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            // Create a new container
            await containerClient.CreateIfNotExistsAsync();

            // Upload a blob
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync("<your_blob_content>");

            // List all blobs in the container
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                Console.WriteLine(blobItem.Name);
            }

            // Update the blob content
            await blobClient.UploadAsync("<your_updated_blob_content>");

            // Delete the blob
            await blobClient.DeleteAsync();

            // Delete the container
            await containerClient.DeleteAsync();
        }
    }
}