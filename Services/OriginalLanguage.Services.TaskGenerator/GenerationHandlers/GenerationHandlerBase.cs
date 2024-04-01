using OriginalLanguage.Services.TaskGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.GenerationHandlers;
internal abstract class GenerationHandlerBase : IGenerationHandler
{
    protected readonly int progressLevel;

    public GenerationHandlerBase(int progressLevel)
    {
        this.progressLevel = progressLevel;
    }

    public Task<string> GenerateQuestion(string sentence)
    {
        return GenerateQuestion(SentenceUtils.SplitToElements(sentence));
    }
    public abstract Task<string> GenerateQuestion(string[] elements);
}
