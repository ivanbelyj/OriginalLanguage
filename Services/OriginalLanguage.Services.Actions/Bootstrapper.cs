using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Actions;
public static class Bootstrapper
{
    public static IServiceCollection AddActions(this IServiceCollection services)
    {
        services.AddSingleton<ISendEmailAction, SendEmailAction>();

        return services;
    }
}
