using System;
using System.Threading.Tasks;
using Azure;
using Azure.Identity;
using Azure.ResourceManager.CosmosDB;
using Azure.ResourceManager.CosmosDB.Models;

class Program
{
    static async Task Main(string[] args)
    {
        string subscriptionId = "<your-subscription-id>";
        string resourceGroupName = "<your-resource-group-name>";
        string accountName = "<your-cosmosdb-account-name>";

        // Authenticate using Azure CLI credentials
        var credential = new DefaultAzureCredential();

        // Create a Cosmos DB management client
        var client = new CosmosDBManagementClient(subscriptionId, credential);

        // Provision a new Cosmos DB account
        var account = new DatabaseAccountCreateUpdateParameters
        {
            Location = "<your-location>",
            Kind = DatabaseAccountKind.GlobalDocumentDB,
            ConsistencyPolicy = new ConsistencyPolicy
            {
                DefaultConsistencyLevel = DefaultConsistencyLevel.Session
            }
        };

        await client.DatabaseAccounts.StartCreateOrUpdateAsync(resourceGroupName, accountName, account);

        Console.WriteLine("Cosmos DB account provisioned successfully!");

        // List all Cosmos DB accounts in the resource group
        var accounts = client.DatabaseAccounts.ListByResourceGroup(resourceGroupName);

        Console.WriteLine("List of Cosmos DB accounts in the resource group:");

        await foreach (var dbAccount in accounts)
        {
            Console.WriteLine(dbAccount.Name);
        }

        // Update the Cosmos DB account
        var updateAccount = new DatabaseAccountUpdateParameters
        {
            Tags = { { "Environment", "Production" } }
        };

        await client.DatabaseAccounts.StartUpdateAsync(resourceGroupName, accountName, updateAccount);

        Console.WriteLine("Cosmos DB account updated successfully!");

        // Delete the Cosmos DB account
        await client.DatabaseAccounts.StartDeleteAsync(resourceGroupName, accountName);

        Console.WriteLine("Cosmos DB account deleted successfully!");
    }
}