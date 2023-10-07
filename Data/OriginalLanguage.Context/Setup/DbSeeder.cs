using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OriginalLanguage.Context.Entities;
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

    public static void SeedDb(IServiceProvider serviceProvider, bool addDemoData)
    {
        if (addDemoData)
        {
            Task.Run(async () =>
            {
                await AddDemoData(serviceProvider);
            });
        }
    }


    private const int languagesCount = 10;
    private static async Task AddDemoData(IServiceProvider serviceProvider)
    {
        await using MainDbContext dbContext = DbContext(serviceProvider);

        if (dbContext.Articles.Any())
            return;

        Random rnd = new Random();

        var addedLanguages = new List<Language>();
        for (int i = 0; i < languagesCount; i++)
        {
            var entity = new Language($"Language {i}", $"Lang native name {i}")
            {
                IsConlang = true,
            };
            addedLanguages.Add(entity);
            dbContext.Languages.Add(entity);
        }

        var addedCourses = new List<Course>();
        for (int i = 0; i < addedLanguages.Count; i++)
        {
            var testCourse = new Course()
            {
                Title = $"Test course {i}",
                Language = addedLanguages[i],
            };
            addedCourses.Add(testCourse);
            dbContext.Courses.Add(testCourse);
        }

        var addedLessons = new List<Lesson>();
        for (int i = 0; i < addedCourses.Count; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                var entity = new Lesson()
                {
                        Number = i,
                        Course = addedCourses[i],
                        TheoryArticle = new Article()
                        {
                            Title = $"Lesson Theory {i}",
                            Content = "Test content",
                        },
                };
                addedLessons.Add(entity);
                dbContext.Lessons.Add(entity);
            }
        }

        var addedLessonSamples = new List<LessonSample>();
        for (int i = 0; i < addedLessons.Count; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                var entity = new LessonSample()
                {
                    MinimalProgressLevel = rnd.Next(3),
                    Lesson = addedLessons[i],
                };
                addedLessonSamples.Add(entity);
                dbContext.LessonSamples.Add(entity);
            }
        }

        for (int i = 0; i < addedLessons.Count; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                var entity = new Sentence()
                {
                    LessonSample = addedLessonSamples[i],
                    Glosses = "Test glosses",
                    LiteralTranslation = "Literal translation",
                    Text = "Текст предложения",
                    Transcription = "Transcription",
                    Translation = "The text of the sentence"
                };
                dbContext.Sentences.Add(entity);
            }
        }

        for (int i = 0; i < addedLessons.Count; i++)
        {
            if (rnd.Next(3) <= 2)
            {
                var entity = new LessonProgress()
                {
                    ProgressLevel = rnd.Next(5),
                    Lesson = addedLessons[i],
                };
                dbContext.LessonProgress.Add(entity);
            }
        }

        for (int i = 0; i < 10; i++)
        {
            var testArticle = new Article()
            {
                Title = $"Test article {i} title",
                Content = "Content of article",
            };
            dbContext.Articles.Add(testArticle);
        }

        dbContext.SaveChanges();
    }
}
