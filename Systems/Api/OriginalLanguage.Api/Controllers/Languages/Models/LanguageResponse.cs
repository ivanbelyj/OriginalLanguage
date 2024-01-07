using AutoMapper;
using OriginalLanguage.Services.Languages.Models;

namespace OriginalLanguage.Api.Controllers.Languages.Models;

public class LanguageResponse
{
    public int Id { get; set; }

    public Guid AuthorId { get; set; }

    public string Name { get; set; }
    public string? NativeName { get; set; }

    public string? About { get; set; }
    public string? AboutNativeSpeakers { get; set; }
    public string? Links { get; set; }

    public string? FlagUrl { get; set; }

    public bool IsConlang { get; set; }
    public ConlangDataModel? ConlangData { get; set; }

    public DateTime DateTimeCreated { get; set; }
    public DateTime DateTimeUpdated { get; set; }
}

public class LanguageResponseProfile : Profile
{
    public LanguageResponseProfile()
    {
        CreateMap<LanguageModel, LanguageResponse>();
            //.ForMember(dest => dest.IsConlang,
            //    opt => opt.MapFrom(src => src.ConlangData != null));
    }
}
