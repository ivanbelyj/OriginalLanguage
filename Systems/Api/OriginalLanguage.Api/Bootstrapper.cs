namespace OriginalLanguage.Api;

using OriginalLanguage.Services.Settings;
using OriginalLanguage.Api.Settings;
using OriginalLanguage.Services.Articles;
using OriginalLanguage.Services.UserAccount;
using OriginalLanguage.Services.Languages;
using OriginalLanguage.Services.Courses;
using OriginalLanguage.Services.Lessons;
using OriginalLanguage.Services.LessonProgresses;

public static class Bootstrapper
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            .AddOpenApiSettings()
            .AddIdentitySettings()
            .AddApiSpecialSettings()

            .AddArticlesService()
            .AddUserAccountService()
            .AddLanguagesService()
            .AddCoursesService()
            .AddLessonsService()
            .AddLessonProgressesService();

        return services;
    }
}
