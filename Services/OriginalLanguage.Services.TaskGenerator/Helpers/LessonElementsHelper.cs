using OriginalLanguage.Common.Lessons;
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
public class LessonElementsHelper
{
    private readonly ILessonSamplesService lessonSamplesService;
    private readonly ISentencesService sentencesService;

    public LessonElementsHelper(
        ILessonSamplesService lessonSamplesService,
        ISentencesService sentencesService)
    {
        this.lessonSamplesService = lessonSamplesService;
        this.sentencesService = sentencesService;
    }

    public async Task<IEnumerable<string>> GetAllLessonElements(
        int lessonId,
        Func<SentenceModel, string?> getProperty)
    {
        var sentences = await GetLessonSentences(lessonId);
        var elements = sentences.SelectMany(
            x => SentenceUtils.SplitToElements(getProperty(x) ?? "", false));
        return elements
            .Select(x => x.ToLower())
            .ToList();
    }

    private async Task<List<SentenceModel>> GetLessonSentences(int lessonId)
    {
        var samples = await lessonSamplesService.GetSamplesOfLesson(lessonId);

        var tasks = samples
            .Select(x => sentencesService.TryGetMainSentenceVariantOrFirst(x));
        return (await Task.WhenAll(tasks)).Where(x => x != null).ToList()!;
    }
}
