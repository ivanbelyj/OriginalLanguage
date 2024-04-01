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
/// Generation of a lesson task based on a certain lesson sample and progress level
/// </summary>
public class TaskGeneratorCore
{
    private readonly ElementsToTranslationHandler elementsToTranslationHandler;
    private readonly ElementsToTextHandler elementsToTextHandler;
    private readonly FillInElementToTextHandler fillInElementToTextHandler;

    public TaskGeneratorCore(
        ElementsToTranslationHandler elementsToTranslationHandler,
        ElementsToTextHandler elementsToTextHandler,
        FillInElementToTextHandler fillInElementToTextHandler)
    {
        this.elementsToTranslationHandler = elementsToTranslationHandler;
        this.elementsToTextHandler = elementsToTextHandler;
        this.fillInElementToTextHandler = fillInElementToTextHandler;
    }

    public async Task<LessonTask> GenerateLessonTask(
        LessonSampleModel lessonSample,
        int progressLevel)
    {
        TaskType taskType = TaskTypeUtils
            .GetRandomTaskTypeByProgressLevel(progressLevel);
        GenerationContext context = new()
        {
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
            TaskType.ElementsToTranslation => elementsToTranslationHandler,
            TaskType.ElementsToText => elementsToTextHandler,
            TaskType.FillInElementToText => fillInElementToTextHandler,
            //TaskType.TextToTranslation => null,
            //TaskType.TranslationToText => null,
            _ => throw new ArgumentException("Invalid task type", nameof(taskType)),
        };
    }
}
