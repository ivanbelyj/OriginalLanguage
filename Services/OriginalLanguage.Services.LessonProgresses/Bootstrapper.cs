using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonProgresses;
public static class Bootstrapper
{
    public static IServiceCollection AddLessonProgressesService(
        this IServiceCollection services)
    {
        services.AddSingleton<ILessonProgressesService, LessonProgressesService>();
        return services;
    }
}
