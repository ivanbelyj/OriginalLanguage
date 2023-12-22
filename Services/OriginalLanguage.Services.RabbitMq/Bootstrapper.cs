using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OriginalLanguage.Consts;
using OriginalLanguage.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.RabbitMq;
public static class Bootstrapper
{
    public static IServiceCollection AddAppRabbitMq(this IServiceCollection services,
        IConfiguration? configuration = null)
    {
        var settings = ConfigurationUtils.Load<RabbitMqSettings>(
            ConfigurationSectionNames.RABBIT_MQ, configuration);
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        services.AddSingleton(settings);

        services.AddSingleton<IRabbitMq, RabbitMq>();

        return services;
    }
}
