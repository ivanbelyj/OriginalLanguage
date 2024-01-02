using Duende.IdentityServer.Services;
using OriginalLanguage.Services.Settings;

namespace OriginalLanguage.Identity.Configuration;

public static class CorsConfiguration
{
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddCors();
        //services.AddCors(options =>
        //{
        //    options.AddPolicy("CorsPolicy",
        //            builder => builder.AllowAnyOrigin()
        //            .AllowAnyMethod()
        //            .AllowAnyHeader());
        //    //.AllowCredentials());
        //});
        //services.AddOptions();

        //services.AddSingleton<ICorsPolicyService>((IServiceProvider container) =>
        //{
        //    var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();

        //    return new DefaultCorsPolicyService(logger)
        //    {
        //        AllowAll = true
        //    };
        //});

        return services;
    }

    public static void UseAppCors(this WebApplication app)
    {
        // Todo: allow not all origins

        //app.UseCors();
        //var mainSettings = app.Services.GetRequiredService<MainSettings>();
        //var origins = mainSettings.AllowedOrigins?
        //    .Split(',', ';')
        //    .Select(x => x.Trim())
        //    .Where(x => !string.IsNullOrEmpty(x)).ToArray();

        app.UseCors(pol =>
        {
            pol.AllowAnyHeader();
            pol.AllowAnyMethod();
            //pol.AllowCredentials();
            //if (origins?.Length > 0)
            //    pol.WithOrigins(origins);
            //else
            //    pol.SetIsOriginAllowed(origin => true);

            pol.AllowAnyOrigin();

            pol.WithExposedHeaders("Content-Disposition");
        });
    }
}