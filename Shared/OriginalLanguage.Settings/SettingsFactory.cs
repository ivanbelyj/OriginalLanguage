namespace OriginalLanguage.Settings;

using Microsoft.Extensions.Configuration;

public static class SettingsFactory
{
    public static IConfiguration Create()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }
}
