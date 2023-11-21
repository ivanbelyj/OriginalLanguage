namespace OriginalLanguage.Api;

using OriginalLanguage.Services.Settings;
using OriginalLanguage.Api.Settings;
using OriginalLanguage.Services.Articles;
using OriginalLanguage.Services.UserAccount;
using OriginalLanguage.Services.Languages;

public static class Bootstrapper
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            .AddOpenApiSettings()
            .AddIdentitySettings()
            .AddApiSpecialSettings()
            .AddArticleService()
            .AddLanguageService()
            .AddUserAccountService();

        return services;
    }
}
