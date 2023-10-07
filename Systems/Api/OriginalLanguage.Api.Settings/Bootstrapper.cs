namespace OriginalLanguage.Api.Settings;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OriginalLanguage.Settings;
using Microsoft.Extensions.Configuration;

public static class Bootstrapper
{
    public static IServiceCollection AddApiSpecialSettings(
        this IServiceCollection services, IConfiguration? configuration = null)
    {
        ApiSpecialSettings? apiSpecialSettings = Configuration.Load<ApiSpecialSettings>(
            "ApiSpecial", configuration);
        services.AddSingleton(apiSpecialSettings);
        return services;
    }
}
