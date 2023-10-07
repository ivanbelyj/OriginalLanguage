namespace OriginalLanguage.Api;

using OriginalLanguage.Services.Settings;
using OriginalLanguage.Api.Settings;
using OriginalLanguage.Services.Articles;

public static class Bootstrapper
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            .AddOpenApiSettings()
            .AddApiSpecialSettings()
            .AddArticleService();

        return services;
    }
}
