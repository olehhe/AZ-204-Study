from azure.identity import DefaultAzureCredential
from azure.keyvault.secrets import SecretClient
from azure.mgmt.keyvault import KeyVaultManagementClient
from azure.mgmt.keyvault.models import AccessPolicyEntry, Permissions, KeyPermissions, SecretPermissions, CertificatePermissions
from azure.mgmt.authorization import AuthorizationManagementClient
from azure.mgmt.authorization.models import RoleAssignmentCreateParameters
from azure.core.exceptions import HttpResponseError

# Set the Azure Key Vault URL and secret name
vault_url = "https://your-key-vault-name.vault.azure.net/"
secret_name = "your-secret-name"

# Create an instance of the DefaultAzureCredential class
credential = DefaultAzureCredential()

# Create an instance of the SecretClient class
client = SecretClient(vault_url=vault_url, credential=credential)

# Get the secret value from the Key Vault
secret_value = client.get_secret(secret_name).value

# Print the secret value
print(secret_value)



# Set the Azure Key Vault name and Azure AD object ID
vault_name = "your-key-vault-name"
object_id = "your-azure-ad-object-id"
role_name = "your-role-name"

# Create an instance of the DefaultAzureCredential class
credential = DefaultAzureCredential()

# Get an access token for the Key Vault
token_request_context = ["https://management.azure.com/.default"]
access_token = credential.get_token(*token_request_context).token

# Create an instance of the KeyVaultManagementClient class
key_vault_client = KeyVaultManagementClient(credential, subscription_id="your-subscription-id")

# Check if the user has the specified role
try:
    role_assignments = key_vault_client.vaults.list_role_assignments(vault_name)
    user_has_role = any(role_assignment.principal_id == object_id and role_assignment.role_definition_name == role_name for role_assignment in role_assignments)

    if user_has_role:
        print("User has the specified role.")
    else:
        print("User does not have the specified role.")
except HttpResponseError as e:
    print(f"An error occurred: {e.message}")
