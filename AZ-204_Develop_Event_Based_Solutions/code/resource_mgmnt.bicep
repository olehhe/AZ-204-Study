param location string = 'eastus'
param serviceBusNamespaceName string = 'myServiceBusNamespace'
param eventHubNamespaceName string = 'myEventHubNamespace'
param eventGridTopicName string = 'myEventGridTopic'

resource serviceBusNamespace 'Microsoft.ServiceBus/namespaces@2021-06-01-preview' = {
  name: serviceBusNamespaceName
  location: location
  sku: {
    name: 'Standard'
    tier: 'Standard'
  }
}

resource eventHubNamespace 'Microsoft.EventHub/namespaces@2021-06-01-preview' = {
  name: eventHubNamespaceName
  location: location
  sku: {
    name: 'Standard'
    tier: 'Standard'
  }
}

resource eventGridTopic 'Microsoft.EventGrid/topics@2021-06-01-preview' = {
  name: eventGridTopicName
  location: location
  sku: {
    name: 'Basic'
  }
}

output serviceBusNamespaceOutput string = serviceBusNamespace.id
output eventHubNamespaceOutput string = eventHubNamespace.id
output eventGridTopicOutput string = eventGridTopic.id
