using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.ContainerRegistry;
using Azure.ResourceManager.ContainerRegistry.Models;

class Program
{
    private static async Task Main(string[] args)
    {
        string subscriptionId = "<your-subscription-id>";
        string resourceGroupName = "<your-resource-group-name>";
        string acrName = "<your-acr-name>";
        var armClient = new ArmClient(new DefaultAzureCredential());

        // Create ACR
        var acrData = new ContainerRegistryData
        {
            Location = "East US",
            Sku = new Sku(ContainerRegistrySkuName.Basic)
        };
        var acrResource = await armClient.GetDefaultSubscription().GetResourceGroups().Get(resourceGroupName).GetContainerRegistries().CreateOrUpdateAsync(acrName, acrData);
        Console.WriteLine($"Created ACR: {acrResource.Value.Data.Name}");

        // Update ACR
        acrResource.Value.Data.Sku = new Sku(ContainerRegistrySkuName.Standard);
        await acrResource.Value.UpdateAsync(acrResource.Value.Data);
        Console.WriteLine($"Updated ACR: {acrResource.Value.Data.Name} to SKU: {acrResource.Value.Data.Sku.Name}");

        // Delete ACR
        await acrResource.Value.DeleteAsync();
        Console.WriteLine($"Deleted ACR: {acrResource.Value.Data.Name}");

        // Create ACI
        var aciData = new ContainerGroupData
        {
            Location = "East US",
            OsType = OperatingSystemType.Linux,
            Containers =
            {
            new ContainerDefinition
            {
                Name = "mycontainer",
                Image = "mcr.microsoft.com/azuredocs/aci-helloworld",
                Resources = new ResourceRequirements
                {
                Requests = new ResourceRequests
                {
                    Cpu = 1,
                    MemoryInGb = 1.5
                }
                }
            }
            },
            RestartPolicy = ContainerGroupRestartPolicy.Always
        };
        var aciResource = await armClient.GetDefaultSubscription().GetResourceGroups().Get(resourceGroupName).GetContainerGroups().CreateOrUpdateAsync("myaci", aciData);
        Console.WriteLine($"Created ACI: {aciResource.Value.Data.Name}");

        // Update ACI
        aciResource.Value.Data.Containers[0].Resources.Requests.MemoryInGb = 2.0;
        await aciResource.Value.UpdateAsync(aciResource.Value.Data);
        Console.WriteLine($"Updated ACI: {aciResource.Value.Data.Name} to Memory: {aciResource.Value.Data.Containers[0].Resources.Requests.MemoryInGb} GB");

        // Delete ACI
        await aciResource.Value.DeleteAsync();
        Console.WriteLine($"Deleted ACI: {aciResource.Value.Data.Name}");

        // Create ACA
        var acaData = new ContainerAppData
        {
            Location = "East US",
            ManagedEnvironmentId = "<your-managed-environment-id>",
            Configuration = new ContainerAppConfiguration
            {
            Ingress = new Ingress
            {
                External = true,
                TargetPort = 80
            }
            },
            Template = new ContainerAppTemplate
            {
            Containers =
            {
                new ContainerAppTemplateContainer
                {
                Name = "myaca",
                Image = "mcr.microsoft.com/azuredocs/containerapp-helloworld",
                Resources = new ContainerAppResourceRequirements
                {
                    Cpu = 0.5,
                    Memory = "1Gi"
                }
                }
            }
            }
        };
        var acaResource = await armClient.GetDefaultSubscription().GetResourceGroups().Get(resourceGroupName).GetContainerApps().CreateOrUpdateAsync("myaca", acaData);
        Console.WriteLine($"Created ACA: {acaResource.Value.Data.Name}");

        // Update ACA
        acaResource.Value.Data.Template.Containers[0].Resources.Memory = "2Gi";
        await acaResource.Value.UpdateAsync(acaResource.Value.Data);
        Console.WriteLine($"Updated ACA: {acaResource.Value.Data.Name} to Memory: {acaResource.Value.Data.Template.Containers[0].Resources.Memory}");

        // Delete ACA
        await acaResource.Value.DeleteAsync();
        Console.WriteLine($"Deleted ACA: {acaResource.Value.Data.Name}");
    }
}