using OriginalLanguage.Common.Lessons;
using OriginalLanguage.Common.Validator;
using OriginalLanguage.Services.LessonSamples;
using OriginalLanguage.Services.LessonSamples.Models;
using OriginalLanguage.Services.Sentences;
using OriginalLanguage.Services.Sentences.Models;
using OriginalLanguage.Services.TaskAnswerChecker.Models;
using OriginalLanguage.Services.TaskGenerator;
using OriginalLanguage.Services.TaskGenerator.Models;
using OriginalLanguage.Services.TaskGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskAnswerChecker;
public class TaskAnswerChecker : ITaskAnswerChecker
{
    private readonly ILessonSamplesService lessonSamplesService;
    private readonly ISentencesService sentencesService;
    private readonly IModelValidator<TaskAnswer> taskAnswerValidator;

    public TaskAnswerChecker(
        ILessonSamplesService lessonSamplesService,
        ISentencesService sentencesService,
        IModelValidator<TaskAnswer> taskAnswerValidator)
    {
        this.lessonSamplesService = lessonSamplesService;
        this.sentencesService = sentencesService;
        this.taskAnswerValidator = taskAnswerValidator;
    }

    /// <summary>
    /// Compares answer with all sentence variant texts / translations
    /// </summary>
    public async Task<CheckAnswerResult> Check(TaskAnswer answer)
    {
        taskAnswerValidator.Check(answer);

        var lessonSample = await lessonSamplesService
            .GetLessonSample(answer.Task.LessonSampleId);
        var sentences = new List<ISentence>() { lessonSample };
        sentences.AddRange(await sentencesService
            .GetLessonSampleSentences(answer.Task.LessonSampleId));

        foreach (var sentence in sentences)
        {
            string? answerString
                = GetAnswerByTaskType(sentence, answer.Task.TaskType);
            if (AreEqualNormalized(answerString ?? "", answer.Answer))
                return await CreateResult(answer.Task, true);
        }
        return await CreateResult(answer.Task, false);
    }

    private async Task<CheckAnswerResult> CreateResult(
        LessonTask lessonTask,
        bool isCorrect)
        => new CheckAnswerResult()
        {
            IsCorrect = isCorrect,
            CorrectAnswer = await GetMainCorrectAnswer(lessonTask)
        };

    private async Task<TaskAnswer?> GetMainCorrectAnswer(LessonTask lessonTask)
    {
        LessonSampleModel lessonSample = await lessonSamplesService
            .GetLessonSample(lessonTask.LessonSampleId);
        return new TaskAnswer()
        {
            Task = lessonTask,
            Answer = GetAnswerByTaskType(lessonSample, lessonTask.TaskType)
        };
    }

    private bool AreEqualNormalized(string str1, string str2) 
        => SentenceUtils.Normalize(str1) == SentenceUtils.Normalize(str2);

    private string? GetAnswerByTaskType(
        ISentence sentence,
        TaskType taskType)
    {
        bool isAnswerLanguageHasTargetRole = TaskTypeUtils
            .IsAnswerLanguageHasTargetRole(taskType);
        return isAnswerLanguageHasTargetRole
            ? sentence.Text
            : sentence.Translation;
    }
}
