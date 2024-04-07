using AutoMapper;
using OriginalLanguage.Services.Sentences.Models;

namespace OriginalLanguage.Api.Controllers.Sentences.Models;

public class AddSentenceRequest
{
    public string? Text { get; set; }
    public string? Translation { get; set; }

    public int LessonSampleId { get; set; }
}

class AddSentenceRequestProfile : Profile
{
    public AddSentenceRequestProfile()
    {
        CreateMap<AddSentenceRequest, AddSentenceModel>();
    }
}
