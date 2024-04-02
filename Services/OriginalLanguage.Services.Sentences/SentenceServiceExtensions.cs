using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.LessonSamples.Models;
using OriginalLanguage.Services.Sentences.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Sentences;
public static class SentenceServiceExtensions
{
    public static async Task<SentenceModel?> TryGetMainSentenceVariantOrFirst(
        this ISentencesService sentencesService,
        LessonSampleModel sample)
    {
        var sampleSentences = await sentencesService
            .GetLessonSampleSentences(sample.Id);
        if (!sampleSentences.Any())
            return null;

        return sample.MainSentenceVariantId == null
            ? sampleSentences.ElementAt(0)
            : await sentencesService
                .GetSentence(sample.MainSentenceVariantId.Value);
    }
}
