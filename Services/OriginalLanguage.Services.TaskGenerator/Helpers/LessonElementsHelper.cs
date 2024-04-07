using OriginalLanguage.Common.Lessons;
using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.Courses;
using OriginalLanguage.Services.Languages;
using OriginalLanguage.Services.Lessons;
using OriginalLanguage.Services.LessonSamples;
using OriginalLanguage.Services.LessonSamples.Models;
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

    public LessonElementsHelper(
        ILessonSamplesService lessonSamplesService)
    {
        this.lessonSamplesService = lessonSamplesService;
    }

    public async Task<IEnumerable<string>> GetAllLessonElements(
        int lessonId,
        Func<ISentence, string?> getProperty)
    {
        var lessonSamples = await lessonSamplesService.GetSamplesOfLesson(lessonId);
        var elements = lessonSamples.SelectMany(
            x => SentenceUtils.SplitToElements(getProperty(x) ?? "", false));
        return elements
            .Select(x => x.ToLower())
            .ToList();
    }
}
