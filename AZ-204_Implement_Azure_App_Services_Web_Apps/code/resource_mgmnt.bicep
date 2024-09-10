param appName string
param location string

resource appServicePlan 'Microsoft.Web/serverfarms@2021-02-01' = {
    name: '${appName}-plan'
    location: location
    sku: {
        name: 'B1'
        tier: 'Basic'
    }
    properties: {
        reserved: true
        targetWorkerCount: 1
    }
}

resource webApp 'Microsoft.Web/sites@2021-02-01' = {
    name: appName
    location: location
    properties: {
        serverFarmId: appServicePlan.id
        siteConfig: {
            appSettings: [
                {
                    name: 'WEBSITE_NODE_DEFAULT_VERSION'
                    value: '14.17.0'
                }
            ]
        }
    }
}

output endpoint string = webApp.properties.defaultHostName
