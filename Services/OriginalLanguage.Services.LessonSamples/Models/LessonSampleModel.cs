using AutoMapper;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonSamples.Models;
public class LessonSampleModel
{
    public int Id { get; set; }
    public int MinimalProgressLevel { get; set; }
    public int? MainSentenceVariantId { get; set; }
    public int LessonId { get; set; }
}

public class LessonSampleModelProfile : Profile
{
    public LessonSampleModelProfile()
    {
        CreateMap<LessonSample, LessonSampleModel>();
    }
}