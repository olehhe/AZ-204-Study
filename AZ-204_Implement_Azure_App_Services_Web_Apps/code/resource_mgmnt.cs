using Azure.Identity;
using Azure.ResourceManager.AppService;
using Azure.ResourceManager.AppService.Models;
using Azure.ResourceManager.Resources;

class Program
{
    static async Task Main(string[] args)
    {
        string subscriptionId = "<your-subscription-id>";
        string resourceGroupName = "<your-resource-group-name>";
        string appName = "<your-app-name>";
        string location = "<your-location>";

        var credential = new DefaultAzureCredential();
        var resourceClient = new ResourcesManagementClient(subscriptionId, credential);
        var appServiceClient = new AppServiceManagementClient(subscriptionId, credential);

        // Create resource group
        await resourceClient.ResourceGroups.CreateOrUpdateAsync(resourceGroupName, new ResourceGroup(location));

        // Create app service plan
        var appServicePlan = new AppServicePlan(location, "S1");
        await appServiceClient.AppServicePlans.CreateOrUpdateAsync(resourceGroupName, appName, appServicePlan);

        // Create app service
        var appService = new WebApp(location);
        await appServiceClient.WebApps.CreateOrUpdateAsync(resourceGroupName, appName, appService);

        // Update app service properties
        var updatedAppService = await appServiceClient.WebApps.GetAsync(resourceGroupName, appName);
        updatedAppService.HttpsOnly = true;
        updatedAppService.Tags.Add("Environment", "Production");
        updatedAppService.Tags.Add("Owner", "John Doe");
        await appServiceClient.WebApps.UpdateAsync(resourceGroupName, appName, updatedAppService);

        // List app services
        var appServices = await appServiceClient.WebApps.ListByResourceGroupAsync(resourceGroupName);
        foreach (var app in appServices)
        {
            Console.WriteLine($"App Service Name: {app.Name}");
            Console.WriteLine($"App Service URL: {app.DefaultHostName}");
            Console.WriteLine();
        }

        // Delete app service
        await appServiceClient.WebApps.StartDeleteAsync(resourceGroupName, appName);
    }
}