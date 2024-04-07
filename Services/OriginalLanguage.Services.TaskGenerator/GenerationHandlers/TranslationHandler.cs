using OriginalLanguage.Services.Sentences;
using OriginalLanguage.Services.TaskGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.GenerationHandlers.Abstract;
public class TranslationHandler : GenerationHandlerBase
{
    public TranslationHandler() : base()
    {
    }

    protected override async Task<LessonTask> GenerateLessonTaskCore()
    {
        return new LessonTask()
        {
            TaskType = Context.TaskType,
            LessonSampleId = Context.LessonSample.Id,
            Given = "",
            Question = await GetQuestion() ?? ""
        };
    }

    private async Task<string?> GetQuestion() {
        return Context.TaskType switch
        {
            TaskType.TextToTranslation => Context.LessonSample.MainText,
            TaskType.TranslationToText => Context.LessonSample.MainTranslation,
            _ => throw new InvalidOperationException("Unsupported task type")
        };
    }
}
