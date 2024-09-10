using System;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;

namespace EventDrivenSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the EventGridClient
            EventGridClient client = new EventGridClient(new TopicCredentials("<topicKey>"));

            // Create an event payload
            var eventPayload = new[]
            {
                new EventGridEvent
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = "MyApp.Events.CustomerCreated",
                    Subject = "customers/123",
                    EventTime = DateTime.UtcNow,
                    DataVersion = "1.0",
                    Data = new { Name = "John Doe", Email = "johndoe@example.com" }
                }
            };

            // Publish the event to the Event Grid topic
            client.PublishEventsAsync("<topicEndpoint>", eventPayload).GetAwaiter().GetResult();

            Console.WriteLine("Event published successfully!");
        }
    }
}