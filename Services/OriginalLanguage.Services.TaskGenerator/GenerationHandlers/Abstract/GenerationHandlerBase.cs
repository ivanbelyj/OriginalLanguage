using OriginalLanguage.Common.Lessons;
using OriginalLanguage.Services.Sentences;
using OriginalLanguage.Services.Sentences.Models;
using OriginalLanguage.Services.TaskGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.GenerationHandlers.Abstract;
public abstract class GenerationHandlerBase : IGenerationHandler
{
    private readonly ISentencesService sentencesService;

    protected GenerationContext Context { get; private set; }
    protected int LessonId => Context.LessonSample.LessonId;

    public GenerationHandlerBase(
        ISentencesService sentencesService)
    {
        this.sentencesService = sentencesService;
    }

    protected abstract Task<LessonTask> GenerateLessonTaskCore(GenerationContext context);
    public Task<LessonTask> GenerateLessonTask(GenerationContext context)
    {
        Context = context;
        return GenerateLessonTaskCore(context);
    }

    protected string[] GetElements(string sentence)
    {
        return SentenceUtils.SplitToElements(SentenceUtils.Normalize(sentence));
    }

    protected async Task<SentenceModel> GetMainSentence()
    {
        var res = await sentencesService
            .TryGetMainSentenceVariantOrFirst(Context.LessonSample);
        if (res == null)
            throw new InvalidOperationException();
        else return res;
    }
}
