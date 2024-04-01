using OriginalLanguage.Services.LessonSamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.GenerationHandlers.Abstract;
public interface IGenerationHandler
{
    Task<LessonTask> GenerateLessonTask(GenerationContext context);
}
