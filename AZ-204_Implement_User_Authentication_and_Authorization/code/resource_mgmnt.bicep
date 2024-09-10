param appName string = 'myApp'
param appRedirectUri string = 'https://myapp.com'
param roleAssignmentName string = 'myRoleAssignment'
param roleDefinitionId string = '00000000-0000-0000-0000-000000000000'
param principalId string = '00000000-0000-0000-0000-000000000001'

resource appRegistration 'Microsoft.Authorization/roleAssignments@2020-08-01-preview' = {
  name: roleAssignmentName
  properties: {
    roleDefinitionId: roleDefinitionId
    principalId: principalId
    principalType: 'ServicePrincipal'
  }
}

resource aadApp 'Microsoft.AzureActiveDirectory/applications@2021-07-01' = {
  name: appName
  properties: {
    displayName: appName
    signInAudience: 'AzureADMyOrg'
    redirectUris: [
      appRedirectUri
    ]
  }
}

