#!/bin/bash

# Set your Azure subscription ID
subscriptionId="<your-subscription-id>"

# Set your resource group name
resourceGroup="<your-resource-group>"

# Set your Event Hubs namespace name
eventHubsNamespace="<your-event-hubs-namespace>"

# Set your Event Hubs name
eventHubsName="<your-event-hubs-name>"

# Set your Event Grid topic name
eventGridTopicName="<your-event-grid-topic-name>"

# Set your Event Grid subscription name
eventGridSubscriptionName="<your-event-grid-subscription-name>"

# Send an event to Event Hubs
az eventhubs eventhub send --hub-name $eventHubsName --namespace-name $eventHubsNamespace --message "Hello, Event Hubs!" --partition-key "partition-key"

# Receive events from Event Hubs
az eventhubs eventhub consumer-group show --hub-name $eventHubsName --namespace-name $eventHubsNamespace --name "$eventHubsName-consumer-group"

# Create an Event Grid topic
az eventgrid topic create --name $eventGridTopicName --resource-group $resourceGroup --location "eastus"

# Create an Event Grid subscription
az eventgrid event-subscription create --name $eventGridSubscriptionName --source-resource-id "/subscriptions/$subscriptionId/resourceGroups/$resourceGroup/providers/Microsoft.EventGrid/topics/$eventGridTopicName" --endpoint "<your-endpoint-url>"