using AutoMapper;
using OriginalLanguage.Services.Languages.Models;

namespace OriginalLanguage.Api.Controllers.Languages.Models;

public class UpdateLanguageRequest
{
    public Guid AuthorId { get; set; }

    public string Name { get; set; }
    public string NativeName { get; set; }

    public string? About { get; set; }
    public string? AboutNativeSpeakers { get; set; }
    public string? Links { get; set; }

    public string? FlagUrl { get; set; }

    public ConlangDataModel? ConlangData { get; set; }
}

public class UpdateLanguageRequestProfile : Profile
{
    public UpdateLanguageRequestProfile()
    {
        CreateMap<UpdateLanguageRequest, UpdateLanguageModel>();
    }
}
