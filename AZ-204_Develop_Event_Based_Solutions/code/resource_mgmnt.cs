using Azure.Identity;
using Azure.Messaging.EventGrid;
using Azure.Messaging.EventHubs;
using Azure.Messaging.ServiceBus;
using Azure.ResourceManager.Resources;
using Azure.ResourceManager.Resources.Models;
using System;
using System.Threading.Tasks;

namespace ResourceManagement
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string subscriptionId = "<your-subscription-id>";
            string resourceGroupName = "<your-resource-group-name>";
            string location = "<your-location>";

            // Authenticate using Azure CLI credentials
            var credential = new DefaultAzureCredential();

            // Create a resource management client
            var resourceClient = new ResourcesManagementClient(subscriptionId, credential);

            // Provision a Service Bus namespace
            var serviceBusNamespaceName = "my-service-bus-namespace";
            var serviceBusNamespace = new ServiceBusNamespace(location);
            await resourceClient.Resources.CreateOrUpdateAsync(resourceGroupName, serviceBusNamespaceName, serviceBusNamespace);

            // Provision an Event Hub
            var eventHubName = "my-event-hub";
            var eventHub = new EventHub(location);
            await resourceClient.Resources.CreateOrUpdateAsync(resourceGroupName, eventHubName, eventHub);

            // Provision an Event Grid topic
            var eventGridTopicName = "my-event-grid-topic";
            var eventGridTopic = new EventGridTopic(location);
            await resourceClient.Resources.CreateOrUpdateAsync(resourceGroupName, eventGridTopicName, eventGridTopic);

            // List all Service Bus namespaces in the resource group
            var serviceBusNamespaces = await resourceClient.Resources.ListByResourceGroupAsync(resourceGroupName, filter: "resourceType eq 'Microsoft.ServiceBus/namespaces'");
            foreach (var serviceBus in serviceBusNamespaces)
            {
                Console.WriteLine(serviceBus.Name);
            }

            // Update the Event Hub
            var updatedEventHub = new EventHub(location) { Sku = new Sku("Standard", "Standard") };
            await resourceClient.Resources.CreateOrUpdateAsync(resourceGroupName, eventHubName, updatedEventHub);

            // Delete the Event Grid topic
            await resourceClient.Resources.StartDeleteAsync(resourceGroupName, eventGridTopicName);
        }
    }
}