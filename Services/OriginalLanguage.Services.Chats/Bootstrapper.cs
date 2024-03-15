using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Chats;
public static class Bootstrapper
{
    public static IServiceCollection AddChatsService(
        this IServiceCollection services,
        IWebHostEnvironment environment)
    {
        services.AddSignalR(options =>
        {
            options.EnableDetailedErrors = environment.IsDevelopment();

        });
        return services;
    }

    public static WebApplication UseChatsService(this WebApplication app)
    {
        app.MapHub<ChatHub>("/chat");
        return app;
    }
}
