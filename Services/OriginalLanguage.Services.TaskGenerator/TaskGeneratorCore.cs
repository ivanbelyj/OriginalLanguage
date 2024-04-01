using OriginalLanguage.Services.LessonSamples.Models;
using OriginalLanguage.Services.Sentences;
using OriginalLanguage.Services.TaskGenerator.Models;
using OriginalLanguage.Services.TaskGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator;

/// <summary>
/// Generation of a lesson task based on a certain lesson sample and progress level
/// </summary>
public class TaskGeneratorCore
{
    private readonly ISentencesService sentencesService;
    private readonly QuestionGenerator questionGenerator;

    public TaskGeneratorCore(
        ISentencesService sentencesService,
        QuestionGenerator questionGenerator)
    {
        this.sentencesService = sentencesService;
        this.questionGenerator = questionGenerator;
    }

    public async Task<LessonTask?> GenerateLessonTask(
        LessonSampleModel lessonSampleModel,
        int progressLevel)
    {
        var (taskType, sentence) = await GetTaskTypeAndSentence(
            lessonSampleModel,
            progressLevel);

        if (sentence == null)
            return null;

        string question = await questionGenerator.GenerateQuestion(
            lessonSampleModel.LessonId,
            taskType,
            progressLevel,
            sentence);

        return new LessonTask()
        {
            LessonSampleId = lessonSampleModel.Id,
            Question = question,
            TaskType = taskType
        };
    }

    private async Task<(TaskType, string?)> GetTaskTypeAndSentence(
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

        return (taskType, sentence);
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
