using OriginalLanguage.Services.Sentences;
using OriginalLanguage.Services.Sentences.Models;
using OriginalLanguage.Services.TaskGenerator.GenerationHandlers.Abstract;
using OriginalLanguage.Services.TaskGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.GenerationHandlers;
public class ElementsToTextHandler : ComposeElementsHandler
{
    public ElementsToTextHandler(
        ISentencesService sentencesService,
        RandomElementsHelper randomElementsHelper)
        : base(sentencesService, randomElementsHelper)
    {
    }

    protected override async Task<LessonTask> GenerateLessonTaskCore(
        GenerationContext context)
    {
        SentenceModel sentence = await GetMainSentence();
        string given = await GenerateGiven(
            sentence,
            sentence => sentence.Text);
        return new()
        {
            Given = given,
            LessonSampleId = Context.LessonSample.Id,
            TaskType = Models.TaskType.ElementsToText,
            Question = sentence.Translation
        };
    }
}
