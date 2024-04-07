using AutoMapper;
using OriginalLanguage.Common.Lessons;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Sentences.Models;
public class SentenceModel : ISentence
{
    public int Id { get; set; }
    public string? Text { get; set; }
    public string? Translation { get; set; }

    public int LessonSampleId { get; set; }
}

public class SentenceModelProfile : Profile
{
    public SentenceModelProfile()
    {
        CreateMap<Sentence, SentenceModel>();
    }
}
