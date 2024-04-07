using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonSamples.Models;
public class AddLessonSampleModel
{
    public int MinimalProgressLevel { get; set; }
    public int? MainSentenceVariantId { get; set; }
    public int LessonId { get; set; }
    public string? MainText { get; set; }
    public string? MainTranslation { get; set; }
    public string? TextHints { get; set; }
    public string? TranslationHints { get; set; }
    public string? Glosses { get; set; }
    public string? Transcription { get; set; }
}

public class AddLessonSampleModelValidator : AbstractValidator<AddLessonSampleModel>
{
    public AddLessonSampleModelValidator()
    {

    }
}

public class AddLessonSampleModelProfile : Profile
{
    public AddLessonSampleModelProfile()
    {
        CreateMap<AddLessonSampleModel, LessonSample>();
    }
}
