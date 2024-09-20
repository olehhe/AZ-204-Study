// ACR
param location string = resourceGroup().location
param registryName string
param sku string = 'Basic'

// Create the Azure Container Registry
resource containerRegistry 'Microsoft.ContainerRegistry/registries@2021-09-01' = {
  name: registryName
  location: location
  sku: {
    name: sku
  }
  properties: {
    adminUserEnabled: true
  }
}

// Output the login server of the container registry
output loginServer string = containerRegistry.properties.loginServer

// ACI
param containerGroupName string
param image string
param cpuCores int = 1
param memoryInGB int = 2

resource containerGroup 'Microsoft.ContainerInstance/containerGroups@2021-07-01' = {
  name: containerGroupName
  location: location
  properties: {
    containers: [
      {
        name: containerGroupName
        properties: {
          image: image
          resources: {
            requests: {
              cpu: cpuCores
              memoryInGB: memoryInGB
            }
          }
        }
      }
    ]
    osType: 'Linux'
  }
}

// ACA
param containerAppName string
param environmentName string

resource containerApp 'Microsoft.App/containerApps@2021-03-01' = {
  name: containerAppName
  location: location
  properties: {
    environmentId: resourceId('Microsoft.App/environments', environmentName)
    configuration: {
      ingress: {
        external: true
        targetPort: 80
      }
    }
    template: {
      containers: [
        {
          name: containerAppName
          properties: {
            image: image
            resources: {
              cpu: cpuCores
              memory: memoryInGB
            }
          }
        }
      ]
    }
  }
}
