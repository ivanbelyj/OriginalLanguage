using AutoMapper;
using OriginalLanguage.Services.Sentences.Models;

namespace OriginalLanguage.Api.Controllers.Sentences.Models;

public class UpdateSentenceRequest
{
    public string? Text { get; set; }
    public string? Translation { get; set; }

    public string? LiteralTranslation { get; set; }

    public string? Glosses { get; set; }
    public string? Transcription { get; set; }

    public int LessonSampleId { get; set; }
}

public class UpdateSentenceRequestProfile : Profile
{
    public UpdateSentenceRequestProfile()
    {
        CreateMap<UpdateSentenceRequest, UpdateSentenceModel>();
    }
}
