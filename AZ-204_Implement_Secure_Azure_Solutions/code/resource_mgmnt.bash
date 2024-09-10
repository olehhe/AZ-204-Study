# Provision a Network Security Group (NSG)
az network nsg create --name my-nsg --resource-group my-resource-group --location eastus

# Update the NSG rules
az network nsg rule create --name allow-http --nsg-name my-nsg --resource-group my-resource-group --priority 100 --protocol Tcp --destination-port-range 80 --access Allow --description "Allow HTTP traffic"

# Provision a Key Vault
az keyvault create --name my-keyvault --resource-group my-resource-group --location eastus

# Delete the NSG
az network nsg delete --name my-nsg --resource-group my-resource-group

# Delete the Key Vault
az keyvault delete --name my-keyvault --resource-group my-resource-group