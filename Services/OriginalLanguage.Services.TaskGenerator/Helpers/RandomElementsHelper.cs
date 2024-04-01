using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.Courses;
using OriginalLanguage.Services.Languages;
using OriginalLanguage.Services.Lessons;
using OriginalLanguage.Services.LessonSamples;
using OriginalLanguage.Services.Sentences;
using OriginalLanguage.Services.Sentences.Models;
using OriginalLanguage.Services.TaskGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.Helpers;
public class RandomElementsHelper
{
    private readonly ILessonsService lessonsService;
    private readonly ILessonSamplesService lessonSamplesService;
    private readonly ISentencesService sentencesService;

    public RandomElementsHelper(
        ILessonsService lessonsService,
        ILessonSamplesService lessonSamplesService,
        ISentencesService sentencesService)
    {
        this.lessonsService = lessonsService;
        this.lessonSamplesService = lessonSamplesService;
        this.sentencesService = sentencesService;
    }

    public async Task<IEnumerable<string>> GetRandomElements(
        int lessonId,
        Func<SentenceModel, string> getProperty,
        int limit)
    {
        var sentences = await GetLessonSentences(lessonId);
        var elements = sentences
            .SelectMany(x => SentenceUtils.SplitToElements(getProperty(x)));
        return elements
            .Shuffled()
            .Take(limit)
            .Select(x => x.ToLower())
            .ToList();
    }

    private async Task<List<SentenceModel>> GetLessonSentences(int lessonId)
    {
        var samples = await lessonSamplesService.GetSamplesOfLesson(lessonId);
        List<SentenceModel> sentences = new();
        foreach (var sample in samples)
        {
            var sentence = await sentencesService
                .TryGetMainSentenceVariantOrFirst(sample);
            if (sentence != null)
                sentences.Add(sentence);
        }
        return sentences;
    }
}
