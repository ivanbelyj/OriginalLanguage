using AutoMapper;
using OriginalLanguage.Services.LessonSamples.Models;

namespace OriginalLanguage.Api.Controllers.LessonSamples.Models;

public class AddLessonSampleRequest
{
    public int MinimalProgressLevel { get; set; }
    public int? MainSentenceVariantId { get; set; }
    public int LessonId { get; set; }
}

public class AddLessonSampleRequestProfile : Profile
{
    public AddLessonSampleRequestProfile()
    {
        CreateMap<AddLessonSampleRequest, AddLessonSampleModel>();
    }
}
