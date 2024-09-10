from azure.servicebus import ServiceBusClient, ServiceBusMessage
from azure.eventhub import EventHubProducerClient, EventData
from azure.eventgrid import EventGridPublisherClient, EventGridEvent

# Interacting with Service Bus
servicebus_conn_str = "<your_servicebus_connection_string>"
servicebus_queue_name = "<your_servicebus_queue_name>"

servicebus_client = ServiceBusClient.from_connection_string(servicebus_conn_str)
with servicebus_client:
    servicebus_sender = servicebus_client.get_queue_sender(queue_name=servicebus_queue_name)
    message = ServiceBusMessage("Hello, Service Bus!")
    servicebus_sender.send_messages(message)

# Interacting with Event Hub
eventhub_conn_str = "<your_eventhub_connection_string>"
eventhub_name = "<your_eventhub_name>"

eventhub_client = EventHubProducerClient.from_connection_string(eventhub_conn_str, eventhub_name=eventhub_name)
with eventhub_client:
    event_data = EventData("Hello, Event Hub!")
    eventhub_client.send(event_data)

# Interacting with Event Grid
eventgrid_endpoint = "<your_eventgrid_endpoint>"
eventgrid_key = "<your_eventgrid_key>"
eventgrid_topic = "<your_eventgrid_topic>"

eventgrid_client = EventGridPublisherClient(eventgrid_endpoint, credential=eventgrid_key)
event = EventGridEvent(
    data={
        "message": "Hello, Event Grid!"
    },
    subject="MySubject",
    event_type="MyEventType",
    data_version="1.0"
)
eventgrid_client.send_events(event)