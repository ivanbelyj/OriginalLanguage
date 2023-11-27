using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Sentences;
public static class Bootstrapper
{
    public static IServiceCollection AddSentencesService(this IServiceCollection services)
    {
        services.AddSingleton<ISentencesService, SentencesService>();
        return services;
    }
}
