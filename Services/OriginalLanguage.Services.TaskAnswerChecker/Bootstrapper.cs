using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskAnswerChecker;
public static class Bootstrapper
{
    public static IServiceCollection AddTaskAnswerChecker(
        this IServiceCollection services)
    {
        services.AddSingleton<ITaskAnswerChecker, TaskAnswerChecker>();
        return services;
    }
}
