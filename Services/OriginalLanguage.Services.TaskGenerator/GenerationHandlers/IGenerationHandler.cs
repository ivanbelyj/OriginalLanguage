using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.GenerationHandlers;
internal interface IGenerationHandler
{
    Task<string> GenerateQuestion(string sentence);
}
