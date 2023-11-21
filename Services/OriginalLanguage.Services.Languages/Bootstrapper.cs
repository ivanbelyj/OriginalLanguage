using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Languages;
public static class Bootstrapper
{
    public static IServiceCollection AddLanguageService(
        this IServiceCollection services)
    {
        services.AddSingleton<ILanguagesService, LanguagesService>();
        return services;
    }
}
