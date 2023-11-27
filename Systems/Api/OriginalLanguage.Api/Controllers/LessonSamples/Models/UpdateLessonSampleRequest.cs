using AutoMapper;
using OriginalLanguage.Services.LessonSamples.Models;

namespace OriginalLanguage.Api.Controllers.LessonSamples.Models;

public class UpdateLessonSampleRequest
{
    public int MinimalProgressLevel { get; set; }
    public int? MainSentenceVariantId { get; set; }
    public int LessonId { get; set; }
}

public class UpdateLessonSampleRequestProfile : Profile
{
    public UpdateLessonSampleRequestProfile()
    {
        CreateMap<UpdateLessonSampleRequest, UpdateLessonSampleModel>();
    }
}
