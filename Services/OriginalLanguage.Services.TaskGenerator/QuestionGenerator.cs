using OriginalLanguage.Services.TaskGenerator.GenerationHandlers;
using OriginalLanguage.Services.TaskGenerator.Helpers;
using OriginalLanguage.Services.TaskGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator;
public class QuestionGenerator
{
    private readonly RandomElementsHelper randomElementsHelper;

    public QuestionGenerator(RandomElementsHelper randomElementsHelper)
    {
        this.randomElementsHelper = randomElementsHelper;
    }

    public async Task<string> GenerateQuestion(
        int lessonId,
        TaskType taskType,
        int progressLevel,
        string sentence)
    {
        IGenerationHandler? handler = taskType switch
        {
            TaskType.ElementsToTranslation => new ElementsToTranslationHandler(progressLevel),
            TaskType.ElementsToText => new ElementsToTextHandler(
                lessonId,
                randomElementsHelper,
                progressLevel),
            TaskType.FillInElementToText => new FillInElementToTextHandler(progressLevel),
            TaskType.TextToTranslation => null,
            TaskType.TranslationToText => null,
            _ => throw new ArgumentException("Invalid task type", nameof(taskType)),
        };

        return handler == null ? sentence : await handler.GenerateQuestion(sentence);
    }
}
