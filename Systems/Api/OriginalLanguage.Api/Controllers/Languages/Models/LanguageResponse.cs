using AutoMapper;
using OriginalLanguage.Services.Languages.Models;

namespace OriginalLanguage.Api.Controllers.Languages.Models;

public class LanguageResponse
{
    public int Id { get; set; }

    public Guid AuthorId { get; set; }

    public string Name { get; set; }
    public string NativeName { get; set; }

    public bool IsConlang { get; set; }
    public DateTime DateTimeAdded { get; set; }
}

public class LanguageResponseProfile : Profile
{
    public LanguageResponseProfile()
    {
        CreateMap<LanguageModel, LanguageResponse>();
    }
}
