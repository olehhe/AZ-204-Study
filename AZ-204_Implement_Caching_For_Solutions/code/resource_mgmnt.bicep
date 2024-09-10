resource symbolicname 'Microsoft.Cache/redis@2023-08-01' = {
  name: 'myRedisCache'
  location: 'eastus'
  tags: {
    tagName1: 'tagValue1'
    tagName2: 'tagValue2'
  }
  identity: {
    type: 'SystemAssigned'
    userAssignedIdentities: {
      customProp: {}
    }
  }
  properties: {
    enableNonSslPort: false
    minimumTlsVersion: '1.2'
    publicNetworkAccess: 'Enabled'
    redisConfiguration: {
      'aad-enabled': 'false'
      'aof-backup-enabled': 'true'
      'aof-storage-connection-string-0': 'connectionString0'
      'aof-storage-connection-string-1': 'connectionString1'
      authnotrequired: 'false'
      'maxfragmentationmemory-reserved': '100mb'
      'maxmemory-delta': '10mb'
      'maxmemory-policy': 'allkeys-lru'
      'maxmemory-reserved': '50mb'
      'preferred-data-persistence-auth-method': 'rdb'
      'rdb-backup-enabled': 'true'
      'rdb-backup-frequency': '60'
      'rdb-backup-max-snapshot-count': '5'
      'rdb-storage-connection-string': 'rdbConnectionString'
      'storage-subscription-id': 'subscriptionId'
    }
    redisVersion: '6.0'
    replicasPerMaster: 1
    replicasPerPrimary: 1
    shardCount: 1
    sku: {
      capacity: 1
      family: 'C'
      name: 'Standard'
    }
    staticIP: '10.0.0.4'
    subnetId: '/subscriptions/subscriptionId/resourceGroups/resourceGroup/providers/Microsoft.Network/virtualNetworks/vnet/subnets/subnet'
    tenantSettings: {}
    updateChannel: 'Default'
  }
}
