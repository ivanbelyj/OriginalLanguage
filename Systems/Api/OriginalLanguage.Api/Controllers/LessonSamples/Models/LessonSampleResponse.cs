using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Services.LessonSamples.Models;

namespace OriginalLanguage.Api.Controllers.LessonSamples.Models;
public class LessonSampleResponse
{
    public int Id { get; set; }
    public int MinimalProgressLevel { get; set; }
    public int LessonId { get; set; }
    public string? MainText { get; set; }
    public string? MainTranslation { get; set; }
    public string? TextHints { get; set; }
    public string? TranslationHints { get; set; }
    public string? Glosses { get; set; }
    public string? Transcription { get; set; }
}

public class LessonSampleResponseProfile : Profile
{
    public LessonSampleResponseProfile()
    {
        CreateMap<LessonSampleModel, LessonSampleResponse>();
    }
}
