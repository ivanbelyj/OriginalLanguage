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
    private GenerationContext? context;

    protected GenerationContext Context {
        get
        {
            if (context == null)
                throw new InvalidOperationException("Using not initialized "
                    + "generation handler");
            else return context;
        }
        private set => context = value;
    }

    protected int LessonId => Context.LessonSample.LessonId;

    public GenerationHandlerBase(
        ISentencesService sentencesService)
    {
        this.sentencesService = sentencesService;
    }

    protected abstract Task<LessonTask> GenerateLessonTaskCore();
    public Task<LessonTask> GenerateLessonTask(GenerationContext context)
    {
        Context = context;
        return GenerateLessonTaskCore();
    }

    protected List<string> GetElements(string sentence)
    {
        return SentenceUtils.SplitToElements(sentence);
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
