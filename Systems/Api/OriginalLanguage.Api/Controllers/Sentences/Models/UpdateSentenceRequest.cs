using AutoMapper;
using OriginalLanguage.Services.Sentences.Models;

namespace OriginalLanguage.Api.Controllers.Sentences.Models;

public class UpdateSentenceRequest
{
    public string? Text { get; set; }
    public string? Translation { get; set; }

    public int LessonSampleId { get; set; }
}

public class UpdateSentenceRequestProfile : Profile
{
    public UpdateSentenceRequestProfile()
    {
        CreateMap<UpdateSentenceRequest, UpdateSentenceModel>();
    }
}
