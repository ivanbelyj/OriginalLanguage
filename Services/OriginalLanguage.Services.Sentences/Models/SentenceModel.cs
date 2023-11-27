using AutoMapper;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Sentences.Models;
public class SentenceModel
{
    public int Id { get; set; }
    public string? Text { get; set; }
    public string? Translation { get; set; }

    public string? LiteralTranslation { get; set; }

    public string? Glosses { get; set; }
    public string? Transcription { get; set; }

    public int LessonSampleId { get; set; }
}

public class SentenceModelProfile : Profile
{
    public SentenceModelProfile()
    {
        CreateMap<Sentence, SentenceModel>();
    }
}
