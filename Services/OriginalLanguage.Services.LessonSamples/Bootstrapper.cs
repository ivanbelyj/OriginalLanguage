using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonSamples;
public static class Bootstrapper
{
    public static IServiceCollection AddLessonSamplesService(this IServiceCollection services)
    {
        services.AddSingleton<ILessonSamplesService, LessonSamplesService>();
        return services;
    }
}
