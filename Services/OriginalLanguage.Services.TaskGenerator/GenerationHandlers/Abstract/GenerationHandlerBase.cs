using OriginalLanguage.Common.Lessons;
using OriginalLanguage.Services.LessonSamples;
using OriginalLanguage.Services.LessonSamples.Models;
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

    public GenerationHandlerBase()
    {
        
    }

    protected abstract Task<LessonTask> GenerateLessonTaskCore();
    public Task<LessonTask> GenerateLessonTask(GenerationContext context)
    {
        Context = context;
        return GenerateLessonTaskCore();
    }

    protected List<string> GetElements(string sentence)
    {
        return SentenceUtils.SplitToElements(sentence, false);
    }
}
