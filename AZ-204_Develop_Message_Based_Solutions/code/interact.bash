# Create a Service Bus topic
az servicebus topic create --name mytopic --namespace-name mynamespace --resource-group myresourcegroup

# Create a Service Bus subscription
az servicebus topic subscription create --name mysubscription --namespace-name mynamespace --resource-group myresourcegroup --topic-name mytopic

# Send a message to a Service Bus topic
az servicebus topic send --name mytopic --namespace-name mynamespace --resource-group myresourcegroup --message "Hello, World!"

# Receive a message from a Service Bus subscription
az servicebus topic subscription receive --name mysubscription --namespace-name mynamespace --resource-group myresourcegroup --topic-name mytopic

# Create an Event Grid topic
az eventgrid topic create --name myeventgridtopic --resource-group myresourcegroup --location westus2

# Create an Event Grid subscription
az eventgrid event-subscription create --name myeventgridsubscription --resource-group myresourcegroup --topic-name myeventgridtopic --endpoint "https://myendpoint.com"

# Publish an event to an Event Grid topic
az eventgrid event publish --name myeventgridtopic --resource-group myresourcegroup --data "{'message': 'Hello, World!'}"

# List all Event Grid topics
az eventgrid topic list --resource-group myresourcegroup

# Delete a Service Bus topic
az servicebus topic delete --name mytopic --namespace-name mynamespace --resource-group myresourcegroup

# Delete a Service Bus subscription
az servicebus topic subscription delete --name mysubscription --namespace-name mynamespace --resource-group myresourcegroup --topic-name mytopic

# Delete an Event Grid topic
az eventgrid topic delete --name myeventgridtopic --resource-group myresourcegroup