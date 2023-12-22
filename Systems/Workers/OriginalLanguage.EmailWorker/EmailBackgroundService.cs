using OriginalLanguage.Consts;
using OriginalLanguage.Services.EmailSender;
using OriginalLanguage.Services.EmailSender.Models;
using OriginalLanguage.Services.RabbitMq;
using System;
using Serilog;

namespace OriginalLanguages.EmailWorker;

public class EmailBackgroundService : BackgroundService
{
    private readonly IRabbitMq rabbitMq;
    private readonly Serilog.ILogger logger;
    private readonly IServiceProvider serviceProvider;

    public EmailBackgroundService(IRabbitMq rabbitMq, Serilog.ILogger logger,
        IServiceProvider serviceProvider)
    {
        this.rabbitMq = rabbitMq;
        this.logger = logger;
        this.serviceProvider = serviceProvider;
    }

    private async Task GetScopedServiceAndExecute<TService>(Func<TService, Task> func)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var service = serviceProvider.GetService<TService>();
            if (service != null)
                await func(service);
            else
                logger.Error($"Error: service {typeof(TService).FullName} wasn`t resolved");
        } catch(Exception ex)
        {
            logger.Error($"Error: {RabbitMqTaskQueueNames.SEND_EMAIL}: {ex.Message}");
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!stoppingToken.IsCancellationRequested)
        {
            logger.Information("Execute EmailBackgroundService");
            rabbitMq.Subscribe<EmailModel>(RabbitMqTaskQueueNames.SEND_EMAIL,
                async data => await GetScopedServiceAndExecute<IEmailSender>(
                    async (emailSender) => {
                        logger.Information($"RABBITMQ::: {RabbitMqTaskQueueNames.SEND_EMAIL}: "
                            + data.Content);
                        await emailSender.Send(data);
                    }));
        }
        return Task.CompletedTask;
    }
}
