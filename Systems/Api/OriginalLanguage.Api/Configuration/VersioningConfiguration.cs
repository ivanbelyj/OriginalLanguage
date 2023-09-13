namespace OriginalLanguage.Api.Configuration;

using Microsoft.AspNetCore.Mvc;

public static class VersioningConfiguration
{
    /// <summary>
    /// Add version support for API
    /// </summary>
    public static IServiceCollection AddAppVersioning(
        this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;

            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}