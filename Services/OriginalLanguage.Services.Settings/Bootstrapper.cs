﻿namespace OriginalLanguage.Services.Settings;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OriginalLanguage.Settings;

public static class Bootstrapper
{
    public static IServiceCollection AddMainSettings(this IServiceCollection services,
        IConfiguration? configuration = null)
    {
        MainSettings? settings = Configuration.Load<MainSettings>("Main", configuration);
        services.AddSingleton(settings);

        return services;
    }


    public static IServiceCollection AddOpenApiSettings(this IServiceCollection services,
        IConfiguration? configuration = null)
    {
        var settings = Configuration.Load<OpenApiSettings>("OpenApi", configuration);
        services.AddSingleton(settings);

        return services;
    }
}
