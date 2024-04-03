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
    public TranslationHandler(ISentencesService sentencesService)
        : base(sentencesService)
    {
    }

    protected override async Task<LessonTask> GenerateLessonTaskCore()
    {
        return new LessonTask()
        {
            TaskType = Context.TaskType,
            LessonSampleId = Context.LessonSample.Id,
            Given = "",
            Question = await GetQuestion()
        };
    }

    private async Task<string?> GetQuestion() {
        var mainSentence = await GetMainSentence();
        return Context.TaskType switch
        {
            TaskType.TextToTranslation => mainSentence.Text,
            TaskType.TranslationToText => mainSentence.Translation,
            _ => throw new InvalidOperationException("Unsupported task type")
        };
    }
}
