namespace OriginalLanguage.Identity.Configuration;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OriginalLanguage.Common;

public static class HealthCheckConfiguration
{
    public static IServiceCollection AddAppHealthChecks(
        this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck<SelfHealthCheck>("OriginalLanguage.Identity");

        return services;
    }

    public static void UseAppHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health");  // Should not depend on the database

        app.MapHealthChecks("/health/detail", new HealthCheckOptions
        {
            ResponseWriter = HealthCheckHelper.WriteHealthCheckResponse,
            AllowCachingResponses = false,
        });
    }
}