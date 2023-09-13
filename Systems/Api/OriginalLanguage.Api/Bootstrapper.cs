namespace OriginalLanguage.Api;

using OriginalLanguage.Services.Settings;
using OriginalLanguage.Api.Settings;

public static class Bootstrapper
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            .AddOpenApiSettings()
            .AddApiSpecialSettings();

        return services;
    }
}
