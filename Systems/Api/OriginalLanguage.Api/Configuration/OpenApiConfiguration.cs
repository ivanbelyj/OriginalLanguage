namespace OriginalLanguage.Api.Configuration;

using OriginalLanguage.Consts;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using OriginalLanguage.Services.Settings;

public static class OpenApiConfiguration
{
    private static string AppTitle = "OriginalLanguage Api";

    public static IServiceCollection AddAppOpenApi(this IServiceCollection services,
        OpenApiSettings openApiSettings, IdentitySettings identitySettings)
    {
        if (!openApiSettings.Enabled)
            return services;

        services
            .AddOptions<SwaggerGenOptions>()
            .Configure<IApiVersionDescriptionProvider>((options, provider) =>
            {
                foreach (var avd in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(avd.GroupName, new OpenApiInfo
                    {
                        Version = avd.GroupName,
                        Title = $"{AppTitle}"
                    });
                }
            });

        services.AddSwaggerGen(options =>
        {
            options.SupportNonNullableReferenceTypes();

            options.UseInlineDefinitionsForEnums();

            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            options.DescribeAllParametersInCamelCase();

            // Is's also necessary to enable the function of generating
            // a documentation file api.xml in the project properties
            var xmlFile = "api.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Name = "Bearer",
                Type = SecuritySchemeType.OAuth2,
                Scheme = "oauth2",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri($"{identitySettings.Url}/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { AppScopes.ArticlesRead, "ArticlesRead" },
                            { AppScopes.ArticlesWrite, "ArticlesWrite" }
                        }
                    }
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            }
                        },
                        new List<string>()
                    }
                });

            options.UseOneOfForPolymorphism();
            options.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

            options.ExampleFilters();
        });

        services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

        services.AddSwaggerGenNewtonsoftSupport();

        return services;
    }


    /// <summary>
    /// Start OpenAPI UI
    /// </summary>
    public static void UseAppOpenApi(this WebApplication app)
    {
        var openApiSettings = app.Services.GetService<OpenApiSettings>();

        if (!openApiSettings?.Enabled ?? false)
            return;

        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger(options =>
        {
            options.RouteTemplate = "api/{documentname}/api.yaml";
        });

        app.UseSwaggerUI(
            options =>
            {
                options.RoutePrefix = "api";
                provider.ApiVersionDescriptions.ToList().ForEach(
                    description => options.SwaggerEndpoint(
                        $"/api/{description.GroupName}/api.yaml",
                        description.GroupName.ToUpperInvariant())
                );

                options.DocExpansion(DocExpansion.List);
                options.DefaultModelsExpandDepth(-1);
                options.OAuthAppName(AppTitle);

                //options.OAuthClientId(openApiSettings?.OAuthClientId ?? "");
                //options.OAuthClientSecret(openApiSettings?.OAuthClientSecret ?? "");
            }
        );
    }
}