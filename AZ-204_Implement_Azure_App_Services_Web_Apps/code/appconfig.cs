using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

public class AppConfig
{
    public IConfiguration Configuration { get; }

    public AppConfig()
    {
        var builder = new ConfigurationBuilder();

        // Add Azure App Configuration as a configuration source
        builder.AddAzureAppConfiguration(options =>
        {
            options.Connect("<connection-string>")
                   .UseFeatureFlags();
        });

        Configuration = builder.Build();
    }

    public string GetSetting(string key)
    {
        return Configuration[key];
    }
}