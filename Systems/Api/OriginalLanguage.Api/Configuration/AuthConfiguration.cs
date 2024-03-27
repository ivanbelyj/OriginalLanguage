using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using OriginalLanguage.Consts;
using OriginalLanguage.Context;
using OriginalLanguage.Services.Settings;
using IdentityServer4.AccessTokenValidation;
using Microsoft.IdentityModel.Tokens;
using OriginalLanguage.Context.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using OriginalLanguage.Api.ResourceBasedAuth;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using OriginalLanguage.Api.Auth;

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
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AppScopes.CoursesLearn,
                policy => {
                    policy.Requirements.Add(new ClaimsAuthorizationRequirement(
                        "scope",
                        new List<string> { AppScopes.ContentWrite }));
                });
            options.AddPolicy(AppScopes.ContentWrite,
                policy => {
                    policy.Requirements.Add(new ClaimsAuthorizationRequirement(
                        "scope",
                        new List<string> { AppScopes.ContentWrite }));
                });
            options.AddPolicy(AuthConstants.OwnsResourcePolicy,
                policy =>
                {
                    policy.Requirements.Add(new OwnsResourceRequirement());
                });
        });

        services.AddSingleton<IAuthorizationHandler, OwnsResourceHandler>();

        return services;
    }

    public static IApplicationBuilder UseAppAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();

        app.UseAuthorization();

        return app;
    }
}
