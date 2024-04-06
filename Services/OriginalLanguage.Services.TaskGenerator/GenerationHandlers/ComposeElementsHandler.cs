using OriginalLanguage.Common.Lessons;
using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.Sentences;
using OriginalLanguage.Services.Sentences.Models;
using OriginalLanguage.Services.TaskGenerator.GenerationHandlers.Abstract;
using OriginalLanguage.Services.TaskGenerator.Helpers;
using OriginalLanguage.Services.TaskGenerator.Models;
using OriginalLanguage.Services.TaskGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OriginalLanguage.Services.TaskGenerator.GenerationHandlers;
public class ComposeElementsHandler : GenerationHandlerBase
{
    private readonly IRandomElementsProvider randomElementsProvider;

    public ComposeElementsHandler(
        ISentencesService sentencesService,
        IRandomElementsProvider randomElementsProvider) : base(sentencesService)
    {
        this.randomElementsProvider = randomElementsProvider;
    }

    protected override async Task<LessonTask> GenerateLessonTaskCore()
    {
        SentenceModel sentence = await GetMainSentence();

        string given = await GenerateGiven(
            sentence,
            ElementOriginPropertyUtils.ByTaskType(Context.TaskType));

        return new()
        {
            TaskType = Context.TaskType,
            LessonSampleId = Context.LessonSample.Id,
            Given = given,
            Question = GetQuestion(sentence),
            Hint = GetHint(sentence),
            Glosses = GetGlosses(sentence),
        };
    }

    protected async Task<string> GenerateGiven(
        SentenceModel sentence,
        ElementOriginProperty elementOriginProperty)
    {
        var getProperty = ElementOriginPropertyUtils.ToFunc(elementOriginProperty);
        var res = GetElements(getProperty(sentence) ?? "").ToList();

        int extraWordsCount = GetRecommendedExtraWordsCount();
        var randomElements = await randomElementsProvider.GetRandomElements(
            LessonId,
            elementOriginProperty,
            extraWordsCount);
        res.AddRange(randomElements);

        return string.Join(" ", res.Shuffled());
    }

    private string? GetHint(SentenceModel sentence)
    {
        string? value = sentence.LiteralTranslation;
        return value == null || Context.TaskType != TaskType.ElementsToTranslation
            ? null : SentenceUtils.ExplicitlySeparated(value);
    }

    private string? GetGlosses(SentenceModel sentence)
    {
        return sentence.Glosses == null
            || Context.TaskType != TaskType.ElementsToTranslation
            ? null : SentenceUtils.ExplicitlySeparated(sentence.Glosses);
    }

    private string? GetQuestion(SentenceModel sentence)
    {
        var question = Context.TaskType switch
        {
            TaskType.ElementsToTranslation => sentence.Text,
            TaskType.ElementsToText => sentence.Translation,
            _ => throw new InvalidOperationException("Not supported task type")
        };
        return question == null
            ? null : SentenceUtils.ExplicitlySeparated(question);
    }

    private int GetRecommendedExtraWordsCount()
    {
        return 5;
    }
}
