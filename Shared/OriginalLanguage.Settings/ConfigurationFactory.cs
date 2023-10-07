namespace OriginalLanguage.Settings;

using Microsoft.Extensions.Configuration;

public static class ConfigurationFactory
{
    public static IConfiguration Create()
    {
        var res = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        return res;
    }
}
