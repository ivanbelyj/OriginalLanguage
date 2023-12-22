using OriginalLanguage.Services.EmailSender;
using OriginalLanguage.Services.RabbitMq;

namespace OriginalLanguages.EmailWorker;

public static class Bootstrapper
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services
            .AddAppRabbitMq()
            .AddEmailSender()
            ;
        services.AddHostedService<EmailBackgroundService>();
        return services;
    }
}
