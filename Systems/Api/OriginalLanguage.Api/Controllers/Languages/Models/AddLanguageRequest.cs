using AutoMapper;
using OriginalLanguage.Services.Languages.Models;

namespace OriginalLanguage.Api.Controllers.Languages.Models;

public class AddLanguageRequest
{
    public Guid AuthorId { get; set; }

    public string Name { get; set; }
    public string NativeName { get; set; }

    public bool IsConlang { get; set; }
}

public class AddLanguageRequestProfile : Profile
{
    public AddLanguageRequestProfile()
    {
        CreateMap<AddLanguageRequest, AddLanguageModel>();
    }
}