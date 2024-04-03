using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OriginalLanguage.Services.Sentences;
using OriginalLanguage.Services.TaskGenerator.GenerationHandlers.Abstract;

namespace OriginalLanguage.Services.TaskGenerator.GenerationHandlers;
public class FillInElementToTextHandler : GenerationHandlerBase
{
    public FillInElementToTextHandler(ISentencesService sentencesService)
        : base(sentencesService)
    {
    }

    protected override async Task<LessonTask> GenerateLessonTaskCore()
    {
        return new LessonTask()
        {
            TaskType = Models.TaskType.FillInElementToText,
            LessonSampleId = Context.LessonSample.Id,
            Given = "Todo",
            Question = "Todo",
        };
    }
}
