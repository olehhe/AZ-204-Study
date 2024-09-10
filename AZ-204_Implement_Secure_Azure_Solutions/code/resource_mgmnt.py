from azure.identity import DefaultAzureCredential
from azure.mgmt.network import NetworkManagementClient
from azure.mgmt.keyvault import KeyVaultManagementClient
from azure.mgmt.resource import ResourceManagementClient

# Set your Azure subscription ID and resource group name
subscription_id = 'your_subscription_id'
resource_group_name = 'your_resource_group_name'

# Set your Azure credentials
credential = DefaultAzureCredential()

# Create a Network Security Group
network_client = NetworkManagementClient(credential, subscription_id)
nsg_params = {
    'location': 'westus',
    'security_rules': []
}
nsg = network_client.network_security_groups.create_or_update(resource_group_name, 'my_nsg', nsg_params)

# Create a Key Vault
keyvault_client = KeyVaultManagementClient(credential, subscription_id)
keyvault_params = {
    'location': 'westus',
    'properties': {
        'sku': {
            'name': 'standard'
        },
        'tenant_id': 'your_tenant_id',
        'access_policies': [],
        'enabled_for_disk_encryption': False,
        'enabled_for_deployment': True,
        'enabled_for_template_deployment': True
    }
}
keyvault = keyvault_client.vaults.create_or_update(resource_group_name, 'my_keyvault', keyvault_params)

# Update the NSG
nsg_params['security_rules'].append({
    'name': 'allow_ssh',
    'protocol': 'Tcp',
    'source_port_range': '*',
    'destination_port_range': '22',
    'source_address_prefix': '*',
    'destination_address_prefix': '*',
    'access': 'Allow',
    'priority': 100,
    'direction': 'Inbound'
})
nsg = network_client.network_security_groups.create_or_update(resource_group_name, 'my_nsg', nsg_params)

# Update the Key Vault
keyvault_params['properties']['access_policies'].append({
    'tenant_id': 'your_tenant_id',
    'object_id': 'your_object_id',
    'permissions': {
        'keys': ['get', 'list', 'create', 'update', 'import', 'delete', 'backup', 'restore', 'recover', 'purge'],
        'secrets': ['get', 'list', 'set', 'delete', 'backup', 'restore', 'recover', 'purge'],
        'certificates': ['get', 'list', 'delete', 'create', 'import', 'update', 'managecontacts', 'getissuers', 'listissuers', 'setissuers', 'deleteissuers', 'manageissuers', 'recover', 'purge']
    }
})
keyvault = keyvault_client.vaults.create_or_update(resource_group_name, 'my_keyvault', keyvault_params)

# Delete the NSG
network_client.network_security_groups.begin_delete(resource_group_name, 'my_nsg').wait()

# Delete the Key Vault
keyvault_client.vaults.begin_delete(resource_group_name, 'my_keyvault').wait()