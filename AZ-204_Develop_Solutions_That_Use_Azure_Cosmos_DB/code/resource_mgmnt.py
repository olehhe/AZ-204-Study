from azure.cosmosdb.sql import CosmosDBClient
from azure.identity import DefaultAzureCredential

# Set the Azure subscription ID and resource group name
subscription_id = "<your-subscription-id>"
resource_group_name = "<your-resource-group-name>"

# Set the Cosmos DB account name and database name
account_name = "<your-cosmosdb-account-name>"
database_name = "<your-database-name>"

# Create a Cosmos DB client
credential = DefaultAzureCredential()
client = CosmosDBClient(credential, subscription_id)

# Provision a Cosmos DB account
def provision_cosmosdb():
    client.cosmosdb_accounts.begin_create_or_update(
        resource_group_name,
        account_name,
        {
            "location": "<your-location>",
            "kind": "GlobalDocumentDB",
            "database_account_offer_type": "Standard",
            "capabilities": [
                {
                    "name": "EnableServerless"
                }
            ]
        }
    ).result()
    print("Cosmos DB account provisioned successfully.")

# List all Cosmos DB accounts in the resource group
def list_cosmosdb_accounts():
    accounts = client.cosmosdb_accounts.list_by_resource_group(resource_group_name)
    for account in accounts:
        print(f"Account name: {account.name}, Location: {account.location}")

# Update the Cosmos DB account
def update_cosmosdb_account():
    account = client.cosmosdb_accounts.get(resource_group_name, account_name)
    account.capabilities.append({"name": "EnableAutomaticFailover"})
    client.cosmosdb_accounts.begin_create_or_update(resource_group_name, account_name, account).result()
    print("Cosmos DB account updated successfully.")

# Delete the Cosmos DB account
def delete_cosmosdb_account():
    client.cosmosdb_accounts.begin_delete(resource_group_name, account_name).result()
    print("Cosmos DB account deleted successfully.")

# Uncomment the function calls below to execute the operations

# provision_cosmosdb()
# list_cosmosdb_accounts()
# update_cosmosdb_account()
# delete_cosmosdb_account()