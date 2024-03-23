using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator;
public static class Bootstrapper
{
    public static IServiceCollection AddTaskGenerator(
        this IServiceCollection services)
    {
        services.AddSingleton<ITaskGenerator, TaskGenerator>();
        return services;
    }
}