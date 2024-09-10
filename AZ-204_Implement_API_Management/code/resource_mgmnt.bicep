param apimName string
param apimLocation string
param apimSku string

resource apim 'Microsoft.ApiManagement/service@2021-01-01-preview' = {
  name: apimName
  location: apimLocation
  sku: {
      name: apimSku
      capacity: 1
  }
  properties: {
    publisherEmail: 'your-email@example.com'
    publisherName: 'Your Name'
    virtualNetworkType: 'None'
  }
}
