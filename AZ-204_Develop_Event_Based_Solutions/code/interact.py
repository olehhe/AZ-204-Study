from azure.eventhub import EventHubProducerClient, EventData

# Connection string to your Event Hub namespace
connection_string = "your_event_hub_connection_string"

# Create an instance of the EventHubProducerClient
producer = EventHubProducerClient.from_connection_string(connection_string)

# Create a batch of events
batch = producer.create_batch()

# Add events to the batch
event_data = EventData("Hello, Event Hub!")
batch.add(event_data)

# Send the batch of events to the Event Hub
producer.send_batch(batch)

# Close the producer client
producer.close()