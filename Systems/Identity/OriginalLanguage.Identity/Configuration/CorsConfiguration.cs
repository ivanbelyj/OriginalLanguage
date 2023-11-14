using Duende.IdentityServer.Services;

namespace OriginalLanguage.Identity.Configuration;

public static class CorsConfiguration
{
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddSingleton<ICorsPolicyService>((IServiceProvider container) =>
        {
            var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();

            return new DefaultCorsPolicyService(logger)
            {
                AllowAll = true
            };
        });
        
        return services;
    }

    //public static void UseAppCors(this IApplicationBuilder app)
    //{
        
    //}
}