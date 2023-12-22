using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.EmailSender;
public static class Bootstrapper
{
    public static IServiceCollection AddEmailSender(
        this IServiceCollection services)
    {
        services.AddSingleton<IEmailSender, EmailSender>();
        return services;
    }
}
