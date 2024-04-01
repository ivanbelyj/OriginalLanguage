using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.GenerationHandlers;
internal class FillInElementToTextHandler : GenerationHandlerBase
{
    public FillInElementToTextHandler(int progressLevel) : base(progressLevel)
    {
        
    }

    public override async Task<string> GenerateQuestion(string[] elements)
    {
        return string.Join(", ", elements);
    }
}
