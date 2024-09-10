using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BlobStorageInteract
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "<your_connection_string>";
            string containerName = "<your_container_name>";
            string blobName = "<your_blob_name>";

            // Create a BlobServiceClient object using the connection string
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            // Create a container client object
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            // Create a blob client object
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            // Upload a file to the blob storage
            await using FileStream uploadFileStream = File.OpenRead("<path_to_local_file>");
            await blobClient.UploadAsync(uploadFileStream, true);

            // Download the blob to a local file
            BlobDownloadInfo download = await blobClient.DownloadAsync();
            await using FileStream downloadFileStream = File.OpenWrite("<path_to_save_file>");
            await download.Content.CopyToAsync(downloadFileStream);
            downloadFileStream.Close();

            // Delete the blob
            await blobClient.DeleteAsync();

            Console.WriteLine("Blob storage interaction completed.");
        }
    }
}