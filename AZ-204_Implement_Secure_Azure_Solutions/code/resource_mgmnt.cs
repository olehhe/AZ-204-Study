using System;
using System.Threading.Tasks;
using Microsoft.Azure.Management.Network;
using Microsoft.Azure.Management.Network.Models;
using Microsoft.Azure.Management.KeyVault;
using Microsoft.Azure.Management.KeyVault.Models;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.Rest;

class Program
{
    static async Task Main(string[] args)
    {
        string subscriptionId = "<Your Subscription ID>";
        string resourceGroupName = "<Your Resource Group Name>";
        string location = "<Your Resource Group Location>";

        // Authenticate using your Azure credentials
        var credentials = SdkContext.AzureCredentialsFactory.FromFile("<Your Azure Credentials File>");

        // Create a Network Security Group
        var networkSecurityGroup = await CreateNetworkSecurityGroup(credentials, subscriptionId, resourceGroupName, location);
        Console.WriteLine("Network Security Group created successfully!");

        // Update the Network Security Group
        await UpdateNetworkSecurityGroup(credentials, subscriptionId, resourceGroupName, networkSecurityGroup.Id);
        Console.WriteLine("Network Security Group updated successfully!");

        // Delete the Network Security Group
        await DeleteNetworkSecurityGroup(credentials, subscriptionId, resourceGroupName, networkSecurityGroup.Id);
        Console.WriteLine("Network Security Group deleted successfully!");

        // Create a Key Vault
        var keyVault = await CreateKeyVault(credentials, subscriptionId, resourceGroupName, location);
        Console.WriteLine("Key Vault created successfully!");

        // Update the Key Vault
        await UpdateKeyVault(credentials, subscriptionId, resourceGroupName, keyVault.Id);
        Console.WriteLine("Key Vault updated successfully!");

        // Delete the Key Vault
        await DeleteKeyVault(credentials, subscriptionId, resourceGroupName, keyVault.Id);
        Console.WriteLine("Key Vault deleted successfully!");
    }

    static async Task<NetworkSecurityGroup> CreateNetworkSecurityGroup(ServiceClientCredentials credentials, string subscriptionId, string resourceGroupName, string location)
    {
        var networkClient = new NetworkManagementClient(credentials) { SubscriptionId = subscriptionId };

        var nsgParams = new NetworkSecurityGroup()
        {
            Location = location,
            SecurityRules = new List<SecurityRule>()
            {
                new SecurityRule()
                {
                    Name = "Allow-SSH",
                    Protocol = SecurityRuleProtocol.Tcp,
                    SourcePortRange = "*",
                    DestinationPortRange = "22",
                    SourceAddressPrefix = "*",
                    DestinationAddressPrefix = "*",
                    Access = SecurityRuleAccess.Allow,
                    Priority = 100,
                    Direction = SecurityRuleDirection.Inbound
                }
            }
        };

        return await networkClient.NetworkSecurityGroups.CreateOrUpdateAsync(resourceGroupName, "MyNSG", nsgParams);
    }

    static async Task UpdateNetworkSecurityGroup(ServiceClientCredentials credentials, string subscriptionId, string resourceGroupName, string nsgId)
    {
        var networkClient = new NetworkManagementClient(credentials) { SubscriptionId = subscriptionId };

        var nsg = await networkClient.NetworkSecurityGroups.GetAsync(resourceGroupName, "MyNSG");
        nsg.SecurityRules.Add(new SecurityRule()
        {
            Name = "Allow-HTTP",
            Protocol = SecurityRuleProtocol.Tcp,
            SourcePortRange = "*",
            DestinationPortRange = "80",
            SourceAddressPrefix = "*",
            DestinationAddressPrefix = "*",
            Access = SecurityRuleAccess.Allow,
            Priority = 200,
            Direction = SecurityRuleDirection.Inbound
        });

        await networkClient.NetworkSecurityGroups.CreateOrUpdateAsync(resourceGroupName, "MyNSG", nsg);
    }

    static async Task DeleteNetworkSecurityGroup(ServiceClientCredentials credentials, string subscriptionId, string resourceGroupName, string nsgId)
    {
        var networkClient = new NetworkManagementClient(credentials) { SubscriptionId = subscriptionId };

        await networkClient.NetworkSecurityGroups.DeleteAsync(resourceGroupName, "MyNSG");
    }

    static async Task<Vault> CreateKeyVault(ServiceClientCredentials credentials, string subscriptionId, string resourceGroupName, string location)
    {
        var keyVaultClient = new KeyVaultManagementClient(credentials) { SubscriptionId = subscriptionId };

        var vaultParams = new VaultCreateOrUpdateParameters()
        {
            Location = location,
            Properties = new VaultProperties()
            {
                Sku = new Sku()
                {
                    Name = SkuName.Standard
                },
                TenantId = "<Your Tenant ID>",
                AccessPolicies = new List<AccessPolicyEntry>()
                {
                    new AccessPolicyEntry()
                    {
                        TenantId = "<Your Tenant ID>",
                        ObjectId = "<Your Object ID>",
                        Permissions = new Permissions()
                        {
                            Keys = new List<string>() { "all" },
                            Secrets = new List<string>() { "all" }
                        }
                    }
                }
            }
        };

        return await keyVaultClient.Vaults.CreateOrUpdateAsync(resourceGroupName, "MyKeyVault", vaultParams);
    }

    static async Task UpdateKeyVault(ServiceClientCredentials credentials, string subscriptionId, string resourceGroupName, string vaultId)
    {
        var keyVaultClient = new KeyVaultManagementClient(credentials) { SubscriptionId = subscriptionId };

        var vault = await keyVaultClient.Vaults.GetAsync(resourceGroupName, "MyKeyVault");
        vault.Properties.Sku.Name = SkuName.Premium;

        await keyVaultClient.Vaults.CreateOrUpdateAsync(resourceGroupName, "MyKeyVault", vault);
    }

    static async Task DeleteKeyVault(ServiceClientCredentials credentials, string subscriptionId, string resourceGroupName, string vaultId)
    {
        var keyVaultClient = new KeyVaultManagementClient(credentials) { SubscriptionId = subscriptionId };

        await keyVaultClient.Vaults.DeleteAsync(resourceGroupName, "MyKeyVault");
    }
}