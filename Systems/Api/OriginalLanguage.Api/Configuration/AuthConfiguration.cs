using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using OriginalLanguage.Consts;
using OriginalLanguage.Context;
using OriginalLanguage.Services.Settings;
using IdentityServer4.AccessTokenValidation;
using Microsoft.IdentityModel.Tokens;
using OriginalLanguage.Context.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace OriginalLanguage.Api.Configuration;

public static class AuthConfiguration
{
    public static IServiceCollection AddAppAuth(this IServiceCollection services,
        IdentitySettings settings)
    {
        IdentityModelEventSource.ShowPII = true;

        services
            .AddIdentity<AppUser, IdentityRole<Guid>>(opt =>
            {
                // Todo: normal password requirements
                opt.Password.RequiredLength = 0;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<MainDbContext>()
            .AddUserManager<UserManager<AppUser>>()
            .AddDefaultTokenProviders();

        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityServerAuthenticationDefaults
                    .AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults
                    .AuthenticationScheme;
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults
                    .AuthenticationScheme;
            })
            .AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = settings.Url.StartsWith("https://");
                options.Authority = settings.Url;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.Audience = "api";

                //options.Events = new JwtBearerEvents()
                //{
                //    OnMessageReceived = context =>
                //    {
                //        var accessToken = context.Request.Query["access_token"];

                //        // If the request is for our hub...
                //        var path = context.HttpContext.Request.Path;
                //        if (!string.IsNullOrEmpty(accessToken) &&
                //            (path.StartsWithSegments("/chat")))
                //        {
                //            context.Token = accessToken;
                //        }
                //        return Task.CompletedTask;
                //    }
                //};
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AppScopes.ArticlesRead,
                policy => policy.RequireClaim("scope", AppScopes.ArticlesRead));
            options.AddPolicy(AppScopes.ArticlesWrite,
                policy => policy.RequireClaim("scope", AppScopes.ArticlesWrite));
        });

        return services;
    }

    public static IApplicationBuilder UseAppAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();

        app.UseAuthorization();

        return app;
    }
}
