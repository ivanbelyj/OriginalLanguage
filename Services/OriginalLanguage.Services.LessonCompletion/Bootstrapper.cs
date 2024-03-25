using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonCompletion;
public static class Bootstrapper
{
    public static IServiceCollection AddLessonCompletionService(
        this IServiceCollection services)
    {
        services.AddSingleton<ILessonCompletionService, LessonCompletionService>();
        return services;
    }
}
