using AutoMapper;
using OriginalLanguage.Context.Entities.Language;
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

    public bool IsConlang { get; set; }
    public DateTime DateTimeAdded { get; set; }
}

public class LanguageModelProfile : Profile
{
    public LanguageModelProfile()
    {
        CreateMap<Language, LanguageModel>();
    }
}