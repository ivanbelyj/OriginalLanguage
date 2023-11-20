using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OriginalLanguage.Context.Entities;
using OriginalLanguage.Context.Entities.User;
using OriginalLanguage.Services.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Setup;
public static class DbSeeder
{
    private static IServiceScope GetServiceScope(IServiceProvider serviceProvider)
        => serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
    private static MainDbContext DbContext(IServiceProvider serviceProvider)
        => GetServiceScope(serviceProvider).ServiceProvider
            .GetRequiredService<IDbContextFactory<MainDbContext>>().CreateDbContext();

    public async static void SeedDb(IServiceProvider serviceProvider, bool addDemoData)
    {
        if (addDemoData)
        {
            using var scope = GetServiceScope(serviceProvider);

            var userAccountService = scope
                .ServiceProvider
                .GetRequiredService<IUserAccountService>();
            var userManager = scope
                .ServiceProvider
                .GetRequiredService<UserManager<AppUser>>();

            string testUserEmail = "test@tst.com";
            int i = 0;
            while (await userManager.FindByEmailAsync(testUserEmail) != null)
            {
                testUserEmail = $"test{++i}@tst.com";
            }

            var accModel = await userAccountService.Create(new RegisterUserAccountModel()
            {
                Email = testUserEmail,
                Name = testUserEmail,
                Password = testUserEmail
            });

            await AddDemoDataForUser(serviceProvider, accModel.Id);

            //Task.Run(async () =>
            //{
            //    await AddDemoDataForUser(serviceProvider, accModel.Id);
            //});
        }
    }

    private const int maxLanguagesCount = 5;
    private static async Task AddDemoDataForUser(IServiceProvider serviceProvider,
        Guid userId)
    {
        await using MainDbContext dbContext = DbContext(serviceProvider);

        if (dbContext.Articles.Any())
            return;

        Random rnd = new Random();

        var addedLanguages = new List<Language>();
        for (int i = 0; i < maxLanguagesCount; i++)
        {
            var entity = new Language($"Language {i}", $"Lang native name {i}")
            {
                IsConlang = true,
                AuthorId = userId
            };
            addedLanguages.Add(entity);
            dbContext.Languages.Add(entity);
        }
        dbContext.SaveChanges();

        var addedCourses = new List<Course>();
        for (int i = 0; i < addedLanguages.Count; i++)
        {
            var testCourse = new Course()
            {
                Title = $"Test course {i}",
                LanguageId = addedLanguages[i].Id,
                AuthorId = userId
            };
            addedCourses.Add(testCourse);
            dbContext.Courses.Add(testCourse);
        }
        dbContext.SaveChanges();

        var addedLessons = new List<Lesson>();
        for (int i = 0; i < addedCourses.Count; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                var entity = new Lesson()
                {
                        Number = i,
                    Course = addedCourses[i],
                    //CourseId = addedCourses[i].Id,
                    TheoryArticle = new Article()
                    {
                        Title = $"Lesson Theory {i}",
                        Content = "Test content",
                        AuthorId = userId
                    },
                };
                addedLessons.Add(entity);
                dbContext.Lessons.Add(entity);
            }
        }
        dbContext.SaveChanges();

        var addedLessonSamples = new List<LessonSample>();
        for (int i = 0; i < addedLessons.Count; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                var entity = new LessonSample()
                {
                    MinimalProgressLevel = rnd.Next(3),
                    LessonId = addedLessons[i].Id
                    //Lesson = addedLessons[i],
                };
                addedLessonSamples.Add(entity);
                dbContext.LessonSamples.Add(entity);
            }
        }
        dbContext.SaveChanges();

        for (int i = 0; i < addedLessons.Count; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                var entity = new Sentence()
                {
                    //LessonSample = addedLessonSamples[i],
                    LessonSampleId = addedLessonSamples[i].Id,
                    Glosses = "Test glosses",
                    LiteralTranslation = "Literal translation",
                    Text = "Текст предложения",
                    Transcription = "Transcription",
                    Translation = "The text of the sentence",
                };
                dbContext.Sentences.Add(entity);
            }
        }
        dbContext.SaveChanges();

        for (int i = 0; i < addedLessons.Count; i++)
        {
            if (rnd.Next(3) <= 2)
            {
                var entity = new LessonProgress()
                {
                    ProgressLevel = rnd.Next(5),
                    /*Lesson = addedLessons[i],*/
                    LessonId = addedLessons[i].Id,
                    UserId = userId,
                };
                dbContext.LessonProgress.Add(entity);
            }
        }
        dbContext.SaveChanges();

        for (int i = 0; i < 10; i++)
        {
            var testArticle = new Article()
            {
                Title = $"Test article {i} title",
                Content = "Content of article",
                AuthorId = userId,
            };
            dbContext.Articles.Add(testArticle);
        }

        dbContext.SaveChanges();
    }
}
