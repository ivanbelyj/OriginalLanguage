using OriginalLanguage.Common.Lessons;
using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.Sentences;
using OriginalLanguage.Services.Sentences.Models;
using OriginalLanguage.Services.TaskGenerator.Helpers;
using OriginalLanguage.Services.TaskGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OriginalLanguage.Services.TaskGenerator.GenerationHandlers.Abstract;
public abstract class ComposeElementsHandler : GenerationHandlerBase
{
    private readonly RandomElementsHelper randomElementsHelper;

    public ComposeElementsHandler(
        ISentencesService sentencesService,
        RandomElementsHelper randomElementsHelper) : base(sentencesService)
    {
        this.randomElementsHelper = randomElementsHelper;
    }

    protected async Task<string> GenerateGiven(
        SentenceModel sentence,
        Func<SentenceModel, string> getProperty)
    {
        var res = GetElements(getProperty(sentence)).ToList();

        var randomElements = await randomElementsHelper
            .GetRandomElements(LessonId, getProperty, 10);
        res.AddRange(randomElements);

        return string.Join(" ", res.Distinct().Shuffled());
    }
}
