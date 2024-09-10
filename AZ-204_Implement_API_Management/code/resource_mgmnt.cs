using System;
using Microsoft.Azure.Management.ApiManagement;
using Microsoft.Azure.Management.ApiManagement.Models;
using Microsoft.Rest;

class Program
{
    static void Main(string[] args)
    {
        string subscriptionId = "<Your-Subscription-Id>";
        string resourceGroupName = "<Your-Resource-Group-Name>";
        string serviceName = "<Your-APIM-Service-Name>";

        // Authenticate using your Azure credentials
        var credentials = SdkContext.AzureCredentialsFactory.FromFile("<Your-Azure-Credentials-File>");

        // Create the APIM management client
        var client = new ApiManagementClient(credentials)
        {
            SubscriptionId = subscriptionId
        };

        // Provision an APIM resource
        var apimResource = new ApiManagementServiceResource
        {
            Location = "West US",
            Sku = new ApiManagementServiceSkuProperties
            {
                Name = SkuType.Developer,
                Capacity = 1
            }
        };

        client.ApiManagementService.CreateOrUpdate(resourceGroupName, serviceName, apimResource);

        // List all APIM resources in the subscription
        var apimResources = client.ApiManagementService.ListBySubscription();

        foreach (var resource in apimResources)
        {
            Console.WriteLine($"APIM Resource: {resource.Name}");
        }

        // Update the APIM resource
        var updatedResource = new ApiManagementServiceResource
        {
            Sku = new ApiManagementServiceSkuProperties
            {
                Name = SkuType.Premium,
                Capacity = 2
            }
        };

        client.ApiManagementService.Update(resourceGroupName, serviceName, updatedResource);

        // Delete the APIM resource
        client.ApiManagementService.Delete(resourceGroupName, serviceName);

        Console.WriteLine("APIM resource provisioning, listing, updating, and deleting completed.");
    }
}