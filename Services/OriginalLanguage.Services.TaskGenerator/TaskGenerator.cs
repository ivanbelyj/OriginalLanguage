using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.Lessons;
using OriginalLanguage.Services.Lessons.Models;
using OriginalLanguage.Services.LessonSamples;
using OriginalLanguage.Services.LessonSamples.Models;
using OriginalLanguage.Services.Sentences;
using OriginalLanguage.Services.Sentences.Models;
using OriginalLanguage.Services.TaskGenerator.Models;
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
    private readonly ISentencesService sentencesService;

    public TaskGenerator(
        ILessonsService lessonsService,
        ILessonSamplesService lessonSamplesService,
        ISentencesService sentencesService)
    {
        this.lessonsService = lessonsService;
        this.lessonSamplesService = lessonSamplesService;
        this.sentencesService = sentencesService;
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
            var lessonTask = await GenerateLessonTask(
                GetRandomLessonSample(lessonSamples, shuffledLessonSamples),
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

    private int GetRecommendedTasksCount(int lessonSamplesCount)
    {
        return Math.Min(
            MaxTasksPerLesson,
            (int)Math.Round(lessonSamplesCount * 1.5f));
    }

    private async Task<LessonTask?> GenerateLessonTask(
        LessonSampleModel lessonSampleModel,
        int progressLevel)
    {
        TaskType taskType = TaskTypeUtils
            .GetRandomTaskTypeByProgressLevel(progressLevel);
        bool isSentenceLanguageHasTargetRole = TaskTypeUtils
            .IsQuestionLanguageHasTargetRole(taskType);
            
        string? sentence = await GetTaskSentenceByLessonSample(
            lessonSampleModel,
            isSentenceLanguageHasTargetRole);
        return sentence == null ? null : new LessonTask()
        {
            LessonSampleId = lessonSampleModel.Id,
            Question = sentence,
            TaskType = taskType
        };
    }

    private async Task<string?> GetTaskSentenceByLessonSample(
        LessonSampleModel sample,
        bool isSentenceLanguageHasTargetRole)
    {
        var sentence = await sentencesService
            .TryGetMainSentenceVariantOrFirst(sample);
        if (sentence == null)
            return null;

        return isSentenceLanguageHasTargetRole
            ? sentence.Text
            : sentence.Translation;
    }
}
