# Provision a Service Bus namespace
az servicebus namespace create --name mynamespace --resource-group myresourcegroup --location westus2

# Create a Service Bus queue
az servicebus queue create --name myqueue --namespace-name mynamespace --resource-group myresourcegroup

# Create an Event Hub
az eventhubs eventhub create --name myeventhub --namespace-name mynamespace --resource-group myresourcegroup

# List all Service Bus queues
az servicebus queue list --namespace-name mynamespace --resource-group myresourcegroup

# Update a Service Bus queue
az servicebus queue update --name myqueue --namespace-name mynamespace --resource-group myresourcegroup --enable-partitioning true

# Delete a Service Bus queue
az servicebus queue delete --name myqueue --namespace-name mynamespace --resource-group myresourcegroup

# Delete an Event Hub
az eventhubs eventhub delete --name myeventhub --namespace-name mynamespace --resource-group myresourcegroup

# Delete the Service Bus namespace
az servicebus namespace delete --name mynamespace --resource-group myresourcegroup

