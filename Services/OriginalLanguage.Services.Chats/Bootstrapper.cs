using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OriginalLanguage.Services.Chats.Configutation;
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
        services.AddScoped<IMessagesService, MessagesService>();

        // Cofigure JWT for SignalR
        services.TryAddEnumerable(ServiceDescriptor
            .Singleton<IPostConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>());

        return services;
    }

    public static WebApplication UseChatsService(this WebApplication app)
    {
        app.MapHub<ChatHub>("/chat");
        return app;
    }
}
