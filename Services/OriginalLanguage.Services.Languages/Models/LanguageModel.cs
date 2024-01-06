using AutoMapper;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Languages.Models;
public class LanguageModel
{
    public int Id { get; set; }

    public Guid AuthorId { get; set; }

    public string Name { get; set; }
    public string NativeName { get; set; }

    public string? About { get; set; }
    public string? AboutNativeSpeakers { get; set; }
    public string? Links { get; set; }

    public string? FlagUrl { get; set; }

    public ConlangDataModel? ConlangData { get; set; }

    public DateTime DateTimeCreated { get; set; }
    public DateTime DateTimeUpdated { get; set; }
}

public class LanguageModelProfile : Profile
{
    public LanguageModelProfile()
    {
        CreateMap<Language, LanguageModel>()
            .ForMember(dest => dest.ConlangData,
                opt => opt.MapFrom(src => src.ConlangData));
    }
}