using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Lessons;
public static class Bootstrapper
{
    public static IServiceCollection AddLessonsService(
        this IServiceCollection services)
    {
        services.AddSingleton<ILessonsService, LessonsService>();
        return services;
    }
}
