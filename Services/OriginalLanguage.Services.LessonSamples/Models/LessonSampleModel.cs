using AutoMapper;
using OriginalLanguage.Common.Lessons;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonSamples.Models;
public class LessonSampleModel : ISentence
{
    public int Id { get; set; }
    public int MinimalProgressLevel { get; set; }
    public int? MainSentenceVariantId { get; set; }
    public int LessonId { get; set; }
    public string? MainText { get; set; }
    public string? MainTranslation { get; set; }
    public string? TextHints { get; set; }
    public string? TranslationHints { get; set; }
    public string? Glosses { get; set; }
    public string? Transcription { get; set; }

    string? ISentence.Text => MainText;
    string? ISentence.Translation => MainTranslation;
}

public class LessonSampleModelProfile : Profile
{
    public LessonSampleModelProfile()
    {
        CreateMap<LessonSample, LessonSampleModel>();
    }
}