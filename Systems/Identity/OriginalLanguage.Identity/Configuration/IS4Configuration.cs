using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using OriginalLanguage.Context;
using OriginalLanguage.Context.Entities;

namespace OriginalLanguage.Identity.Configuration;

public static class IS4Configuration
{
    public static IServiceCollection AddAppIS4Configuration(
        this IServiceCollection services)
    {
        services
            .AddIdentity<AppUser, IdentityRole<Guid>>(opts =>
            {
                opts.Password.RequiredLength = 8;
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<MainDbContext>()
            .AddUserManager<UserManager<AppUser>>()
            .AddDefaultTokenProviders();

        services
            .AddIdentityServer()

            .AddAspNetIdentity<AppUser>()

            .AddInMemoryApiScopes(AppApiScopes.ApiScopes)
            .AddInMemoryClients(AppClients.Clients)
            .AddInMemoryApiResources(AppResources.Resources)
            .AddInMemoryIdentityResources(AppIdentityResources.Resources)

            //.AddTestUsers(AppApiTestUsers.ApiTestUsers)

            .AddDeveloperSigningCredential();

        return services;
    }

    public static IApplicationBuilder UseAppIS4(this IApplicationBuilder app)
    {
        app.UseIdentityServer();
        return app;
    }
}
