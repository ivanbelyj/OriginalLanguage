using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.Lessons;
using OriginalLanguage.Services.Lessons.Models;
using OriginalLanguage.Services.LessonSamples;
using OriginalLanguage.Services.LessonSamples.Models;
using OriginalLanguage.Services.Sentences;
using OriginalLanguage.Services.Sentences.Models;
using OriginalLanguage.Services.TaskGenerator.Models;
using OriginalLanguage.Services.TaskGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator;

public class TaskGenerator : ITaskGenerator
{
    internal const int MaxTasksPerLesson = 20;

    private readonly ILessonsService lessonsService;
    private readonly ILessonSamplesService lessonSamplesService;
    private readonly TaskGeneratorCore taskGeneratorCore;

    public TaskGenerator(
        ILessonsService lessonsService,
        ILessonSamplesService lessonSamplesService,
        TaskGeneratorCore taskGeneratorCore)
    {
        this.lessonsService = lessonsService;
        this.lessonSamplesService = lessonSamplesService;
        this.taskGeneratorCore = taskGeneratorCore;
    }

    public async Task<IEnumerable<LessonTask>> GenerateLessonTasks(
        int lessonId,
        int progressLevel)
    {
        LessonModel lesson = await lessonsService.GetLesson(lessonId);
        var lessonSamples = (await lessonSamplesService
            .GetSamplesOfLesson(lesson.Id))
            .Where(x => progressLevel >= x.MinimalProgressLevel)
            .ToList();

        int recommendedTasksCount = GetRecommendedTasksCount(lessonSamples.Count());

        List<LessonTask> res = new();
        for (int i = 0; i < 1; i++)
        {
            res.AddRange(await TryGenerateAndAddTasks(
                lessonSamples,
                progressLevel,
                recommendedTasksCount - res.Count));
        }
        
        return res;
    }

    private async Task<List<LessonTask>> TryGenerateAndAddTasks(
        List<LessonSampleModel> lessonSamples,
        int progressLevel,
        int attemptsCount)
    {
        List<LessonTask> tasks = new();
        Stack<LessonSampleModel> shuffledLessonSamples = new();
        for (int i = 0; i < attemptsCount; i++)
        {
            var sample = GetRandomLessonSample(lessonSamples, shuffledLessonSamples);
            var lessonTask = await taskGeneratorCore.GenerateLessonTask(
                sample,
                progressLevel);
            
            if (lessonTask != null)
                tasks.Add(lessonTask);
        }

        return tasks;

        LessonSampleModel GetRandomLessonSample(
            List<LessonSampleModel> source,
            Stack<LessonSampleModel> stack)
        {
            // The stack may be populated several times
            // when the attempts count is more than lessonSamples.Count
            if (stack.Count == 0)
                source
                    .Shuffled()
                    .ForEach(stack.Push);
            return stack.Pop();
        }
    }

    private static int GetRecommendedTasksCount(int lessonSamplesCount)
    {
        return Math.Min(
            MaxTasksPerLesson,
            (int)Math.Round(lessonSamplesCount * 1.5f));
    }
}
