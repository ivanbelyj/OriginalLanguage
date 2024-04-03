using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.LessonSamples.Models;
using OriginalLanguage.Services.TaskGenerator.GenerationHandlers;
using OriginalLanguage.Services.TaskGenerator.GenerationHandlers.Abstract;
using OriginalLanguage.Services.TaskGenerator.Helpers;
using OriginalLanguage.Services.TaskGenerator.Models;
using OriginalLanguage.Services.TaskGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator;

/// <summary>
/// Generates a lesson task based on a certain lesson sample and progress level
/// </summary>
public class TaskGeneratorCore
{
    private readonly ComposeElementsHandler composeElementsHandler;
    private readonly FillInElementToTextHandler fillInElementToTextHandler;
    private readonly TranslationHandler translationHandler;

    public TaskGeneratorCore(
        ComposeElementsHandler composeElementsHandler,
        FillInElementToTextHandler fillInElementToTextHandler,
        TranslationHandler translationHandler)
    {
        this.composeElementsHandler = composeElementsHandler;
        this.fillInElementToTextHandler = fillInElementToTextHandler;
        this.translationHandler = translationHandler;
    }

    public async Task<LessonTask> GenerateLessonTask(
        LessonSampleModel lessonSample,
        int progressLevel)
    {
        TaskType taskType = TaskTypeUtils
            .GetRandomTaskTypeByProgressLevel(progressLevel);
        GenerationContext context = new()
        {
            TaskType = taskType,
            LessonSample = lessonSample,
            ProgressLevel = progressLevel
        };

        IGenerationHandler? handler = GetHandlerByTaskType(taskType);

        return await handler.GenerateLessonTask(context);
    }

    private IGenerationHandler GetHandlerByTaskType(
        TaskType taskType)
    {
        return taskType switch
        {
            TaskType.ElementsToTranslation => composeElementsHandler,
            TaskType.ElementsToText => composeElementsHandler,
            TaskType.FillInElementToText => fillInElementToTextHandler,
            TaskType.TextToTranslation => translationHandler,
            TaskType.TranslationToText => translationHandler,
            _ => throw new ArgumentException("Invalid task type", nameof(taskType)),
        };
    }
}
