using Microsoft.Extensions.DependencyInjection;
using OriginalLanguage.Services.TaskGenerator.GenerationHandlers;
using OriginalLanguage.Services.TaskGenerator.GenerationHandlers.Abstract;
using OriginalLanguage.Services.TaskGenerator.Helpers;
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
        services.AddScoped<IRandomElementsProvider, RandomElementsProvider>();
        services.AddScoped<LessonElementsHelper>();
        services.AddScoped<TaskGeneratorCore>();

        services.AddScoped<ComposeElementsHandler>();
        services.AddScoped<FillInElementToTextHandler>();
        services.AddScoped<TranslationHandler>();

        services.AddScoped<ITaskGenerator, TaskGenerator>();
        return services;
    }
}