using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OriginalLanguage.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Cache;
public static class Bootstrapper
{
    public static IServiceCollection AddCacheService(
        this IServiceCollection services,
        IConfiguration? configuration = null)
    {
        CacheSettings? settings = ConfigurationUtils.Load<CacheSettings>("Cache", configuration);
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));

        services.AddSingleton(settings);
        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
}
