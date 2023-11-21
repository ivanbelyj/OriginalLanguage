using AutoMapper;
using OriginalLanguage.Services.Languages.Models;

namespace OriginalLanguage.Api.Controllers.Languages.Models;

public class UpdateLanguageRequest
{
    public Guid AuthorId { get; set; }

    public string Name { get; set; }
    public string NativeName { get; set; }

    public bool IsConlang { get; set; }
}

public class UpdateLanguageRequestProfile : Profile
{
    public UpdateLanguageRequestProfile()
    {
        CreateMap<UpdateLanguageRequest, UpdateLanguageModel>();
    }
}
