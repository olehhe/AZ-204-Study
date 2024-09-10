using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

class Program
{
    static async Task Main(string[] args)
    {
        // Create a new secret client using the default Azure credentials
        var secretClient = new SecretClient(new Uri("https://your-key-vault-name.vault.azure.net/"), new DefaultAzureCredential());

        // Retrieve a secret from the Key Vault
        KeyVaultSecret secret = await secretClient.GetSecretAsync("your-secret-name");

        // Access the secret value
        string secretValue = secret.Value;

        // Use the secret value in your application
        Console.WriteLine($"Secret value: {secretValue}");

        // RBAC: Check if the current user has a specific role
        var tokenCredential = new DefaultAzureCredential();
        var tokenRequestContext = new TokenRequestContext(new[] { "https://your-key-vault-name.vault.azure.net/.default" });
        var accessToken = await tokenCredential.GetTokenAsync(tokenRequestContext);
        var token = accessToken.Token;

        var keyVaultManagementClient = new KeyVaultManagementClient(new TokenCredentials(token));
        var userHasRole = await keyVaultManagementClient.Vaults.CheckAccessAsync("your-key-vault-name", "your-azure-ad-object-id", new[] { "your-role-name" });

        if (userHasRole.Value)
        {
            Console.WriteLine("User has the specified role.");
        }
        else
        {
            Console.WriteLine("User does not have the specified role.");
        }
    }
}