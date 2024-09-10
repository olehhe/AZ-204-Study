from azure.identity import DefaultAzureCredential
from azure.mgmt.servicebus import ServiceBusManagementClient
from azure.mgmt.eventhub import EventHubManagementClient
from azure.mgmt.eventgrid import EventGridManagementClient
from azure.mgmt.resource import ResourceManagementClient

# Set your Azure subscription ID and resource group name
subscription_id = "<your-subscription-id>"
resource_group_name = "<your-resource-group-name>"

# Set your Azure credentials
credential = DefaultAzureCredential()

# Create the Service Bus management client
servicebus_client = ServiceBusManagementClient(credential, subscription_id)

# Provision a Service Bus namespace
namespace_name = "<your-namespace-name>"
namespace_params = {
    "location": "<your-location>",
    "sku": {"name": "Standard"},
    "tags": {"key": "value"}
}
servicebus_client.namespaces.create_or_update(resource_group_name, namespace_name, namespace_params)

# List all Service Bus namespaces in the resource group
namespaces = servicebus_client.namespaces.list_by_resource_group(resource_group_name)
for namespace in namespaces:
    print(namespace.name)

# Update the Service Bus namespace
updated_params = {
    "tags": {"new-key": "new-value"}
}
servicebus_client.namespaces.update(resource_group_name, namespace_name, updated_params)

# Delete the Service Bus namespace
servicebus_client.namespaces.delete(resource_group_name, namespace_name)

# Create the Event Hub management client
eventhub_client = EventHubManagementClient(credential, subscription_id)

# Provision an Event Hub namespace
eventhub_namespace_name = "<your-eventhub-namespace-name>"
eventhub_namespace_params = {
    "location": "<your-location>",
    "sku": {"name": "Standard"},
    "tags": {"key": "value"}
}
eventhub_client.namespaces.create_or_update(resource_group_name, eventhub_namespace_name, eventhub_namespace_params)

# List all Event Hub namespaces in the resource group
eventhub_namespaces = eventhub_client.namespaces.list_by_resource_group(resource_group_name)
for eventhub_namespace in eventhub_namespaces:
    print(eventhub_namespace.name)

# Update the Event Hub namespace
eventhub_updated_params = {
    "tags": {"new-key": "new-value"}
}
eventhub_client.namespaces.update(resource_group_name, eventhub_namespace_name, eventhub_updated_params)

# Delete the Event Hub namespace
eventhub_client.namespaces.delete(resource_group_name, eventhub_namespace_name)

# Create the Event Grid management client
eventgrid_client = EventGridManagementClient(credential, subscription_id)

# Provision an Event Grid topic
eventgrid_topic_name = "<your-eventgrid-topic-name>"
eventgrid_topic_params = {
    "location": "<your-location>",
    "sku": {"name": "Standard"},
    "tags": {"key": "value"}
}
eventgrid_client.topics.create_or_update(resource_group_name, eventgrid_topic_name, eventgrid_topic_params)

# List all Event Grid topics in the resource group
eventgrid_topics = eventgrid_client.topics.list_by_resource_group(resource_group_name)
for eventgrid_topic in eventgrid_topics:
    print(eventgrid_topic.name)

# Update the Event Grid topic
eventgrid_updated_params = {
    "tags": {"new-key": "new-value"}
}
eventgrid_client.topics.update(resource_group_name, eventgrid_topic_name, eventgrid_updated_params)

# Delete the Event Grid topic
eventgrid_client.topics.delete(resource_group_name, eventgrid_topic_name)