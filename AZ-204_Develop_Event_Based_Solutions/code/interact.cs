using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventGrid;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace EventBasedSystem
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "<Event Hubs connection string>";
            string eventHubName = "<Event Hub name>";

            await SendEventToEventHub(connectionString, eventHubName);
            await PublishEventToEventGrid();
        }

        static async Task SendEventToEventHub(string connectionString, string eventHubName)
        {
            await using (var producerClient = new EventHubProducerClient(connectionString, eventHubName))
            {
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

                // Create and add events to the batch
                EventData eventData = new EventData(Encoding.UTF8.GetBytes("Hello, Event Hub!"));
                eventBatch.TryAdd(eventData);

                // Send the batch of events to the event hub
                await producerClient.SendAsync(eventBatch);
                Console.WriteLine("Event sent to Event Hub.");
            }
        }

        static async Task PublishEventToEventGrid()
        {
            string topicEndpoint = "<Event Grid topic endpoint>";
            string topicKey = "<Event Grid topic key>";

            await using (var client = new EventGridPublisherClient(new Uri(topicEndpoint), new AzureKeyCredential(topicKey)))
            {
                var eventGridEvent = new EventGridEvent(
                    subject: "MyApp.Events",
                    eventType: "MyApp.MyEvent",
                    dataVersion: "1.0",
                    data: new { Message = "Hello, Event Grid!" });

                await client.SendEventAsync(eventGridEvent);
                Console.WriteLine("Event published to Event Grid.");
            }
        }
    }
}