using Azure.Messaging.ServiceBus;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventGrid;

// Interacting with Service Bus
string connectionString = "<your-service-bus-connection-string>";
string queueName = "<your-queue-name>";

await using (ServiceBusClient client = new ServiceBusClient(connectionString))
{
    ServiceBusSender sender = client.CreateSender(queueName);
    ServiceBusReceiver receiver = client.CreateReceiver(queueName);

    // Send a message
    ServiceBusMessage message = new ServiceBusMessage("Hello, Service Bus!");
    await sender.SendMessageAsync(message);

    // Receive a message
    ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();
    string messageBody = receivedMessage.Body.ToString();
    Console.WriteLine($"Received message: {messageBody}");
}

// Interacting with Event Hub
string eventHubConnectionString = "<your-event-hub-connection-string>";
string eventHubName = "<your-event-hub-name>";

await using (EventHubProducerClient producer = new EventHubProducerClient(eventHubConnectionString, eventHubName))
{
    // Send an event
    EventData eventData = new EventData(Encoding.UTF8.GetBytes("Hello, Event Hub!"));
    await producer.SendAsync(eventData);
}

await using (EventHubConsumerClient consumer = new EventHubConsumerClient(EventHubConsumerClient.DefaultConsumerGroupName, eventHubConnectionString, eventHubName))
{
    // Receive events
    await foreach (PartitionEvent partitionEvent in consumer.ReadEventsAsync())
    {
        string eventData = Encoding.UTF8.GetString(partitionEvent.Data.Body.ToArray());
        Console.WriteLine($"Received event: {eventData}");
    }
}

// Interacting with Event Grid
string topicEndpoint = "<your-event-grid-topic-endpoint>";
string topicKey = "<your-event-grid-topic-key>";

EventGridPublisherClient client = new EventGridPublisherClient(new Uri(topicEndpoint), new AzureKeyCredential(topicKey));

// Publish an event
EventGridEvent eventGridEvent = new EventGridEvent
{
    Id = Guid.NewGuid().ToString(),
    EventType = "MyApp.Events.CustomEvent",
    Subject = "MyApp/Orders",
    DataVersion = "1.0",
    Data = new { OrderId = 123, Product = "ABC" }
};

await client.SendEventAsync(eventGridEvent);