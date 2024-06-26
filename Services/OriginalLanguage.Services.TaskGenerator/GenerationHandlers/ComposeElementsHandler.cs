﻿using OriginalLanguage.Common.Lessons;
using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.LessonSamples.Models;
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
        IRandomElementsProvider randomElementsProvider) : base()
    {
        this.randomElementsProvider = randomElementsProvider;
    }

    protected override async Task<LessonTask> GenerateLessonTaskCore()
    {
        LessonSampleModel lessonSample = Context.LessonSample;
        string given = await GenerateGiven(
            lessonSample,
            ElementOriginPropertyUtils.ByTaskType(Context.TaskType));

        return new()
        {
            TaskType = Context.TaskType,
            LessonSampleId = Context.LessonSample.Id,
            Given = given,
            Question = GetQuestion(lessonSample),
            Hint = GetHint(lessonSample),
            Glosses = GetGlosses(lessonSample),
        };
    }

    protected async Task<string> GenerateGiven(
        LessonSampleModel lessonSample,
        ElementOriginProperty elementOriginProperty)
    {
        var getProperty = ElementOriginPropertyUtils.ToFunc(elementOriginProperty);
        var res = GetElements(getProperty(lessonSample) ?? "").ToList();

        int extraWordsCount = GetRecommendedExtraWordsCount();
        var randomElements = (await randomElementsProvider.GetRandomElements(
            LessonId,
            elementOriginProperty,
            extraWordsCount))
            .Except(res)
            .ToList();
        res.AddRange(randomElements);

        return string.Join(" ", res.Shuffled());
    }

    private string? GetHint(LessonSampleModel lessonSample)
    {
        string? value = lessonSample.TextHints;
        return value == null || Context.TaskType != TaskType.ElementsToTranslation
            ? null : SentenceUtils.ExplicitlySeparated(value);
    }

    private string? GetGlosses(LessonSampleModel lessonSample)
    {
        return lessonSample.Glosses == null
            || Context.TaskType != TaskType.ElementsToTranslation
            ? null : SentenceUtils.ExplicitlySeparated(lessonSample.Glosses);
    }

    private string? GetQuestion(LessonSampleModel sample)
    {
        var question = Context.TaskType switch
        {
            TaskType.ElementsToTranslation => sample.MainText,
            TaskType.ElementsToText => sample.MainTranslation,
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
