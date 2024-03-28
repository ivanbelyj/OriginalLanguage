namespace OriginalLanguage.Api;

using OriginalLanguage.Services.Settings;
using OriginalLanguage.Api.Settings;
using OriginalLanguage.Services.Articles;
using OriginalLanguage.Services.UserAccount;
using OriginalLanguage.Services.Languages;
using OriginalLanguage.Services.Courses;
using OriginalLanguage.Services.Lessons;
using OriginalLanguage.Services.LessonProgresses;
using OriginalLanguage.Services.LessonSamples;
using OriginalLanguage.Services.Sentences;
using OriginalLanguage.Services.EmailSender;
using OriginalLanguage.Services.RabbitMq;
using OriginalLanguage.Services.Cache;
using OriginalLanguage.Services.Actions;
using OriginalLanguage.Services.TaskGenerator;
using OriginalLanguage.Services.TaskAnswerChecker;
using OriginalLanguage.Services.LessonCompletion;
using OriginalLanguage.Api.Helpers;

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
            .AddLessonProgressesService()
            .AddLessonSamplesService()
            .AddSentencesService()

            .AddTaskGenerator()
            .AddTaskAnswerChecker()
            .AddLessonCompletionService()
            
            .AddActions()
            .AddCacheService()
            .AddAppRabbitMq()
            
            .AddSingleton<ResourceOwningHelper>();

        return services;
    }
}
