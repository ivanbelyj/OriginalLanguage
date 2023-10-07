namespace OriginalLanguage.Api.Configuration;

using OriginalLanguage.Common;

public static class ControllersAndViewsConfiguration
{
    public static IServiceCollection AddAppControllersAndViews(
        this IServiceCollection services)
    {
        services
            .AddRazorPages();

        services
            .AddControllers()
            .AddNewtonsoftJson(options
                => options.SerializerSettings.SetDefaultSettings())
            .AddValidator();

        return services;
    }

    public static IEndpointRouteBuilder UseAppControllersAndViews(
        this IEndpointRouteBuilder app)
    {
        app.MapRazorPages();
        app.MapControllers();

        return app;
    }
}