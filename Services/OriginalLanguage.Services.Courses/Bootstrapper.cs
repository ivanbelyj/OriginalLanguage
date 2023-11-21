using Microsoft.Extensions.DependencyInjection;

namespace OriginalLanguage.Services.Courses;

public static class Bootstrapper
{
    public static IServiceCollection AddCoursesService(
        this IServiceCollection services)
    {
        services.AddSingleton<ICoursesService, CoursesService>();
        return services;
    }
}
