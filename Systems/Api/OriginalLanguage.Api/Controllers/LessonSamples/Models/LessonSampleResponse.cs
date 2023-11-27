using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Services.LessonSamples.Models;

namespace OriginalLanguage.Api.Controllers.LessonSamples.Models;
public class LessonSampleResponse
{
    public int Id { get; set; }
    public int MinimalProgressLevel { get; set; }
    public int? MainSentenceVariantId { get; set; }
    public int LessonId { get; set; }
}

public class LessonSampleResponseProfile : Profile
{
    public LessonSampleResponseProfile()
    {
        CreateMap<LessonSampleModel, LessonSampleResponse>();
    }
}
