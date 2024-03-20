using AutoMapper;
using FluentValidation;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonSamples.Models;
public class UpdateLessonSampleModel
{
    public int MinimalProgressLevel { get; set; }
    public int? MainSentenceVariantId { get; set; }
    public int LessonId { get; set; }
}

public class UpdateLessonSampleModelValidator : AbstractValidator<UpdateLessonSampleModel>
{
    public UpdateLessonSampleModelValidator()
    {

    }
}

public class UpdateLessonSampleModelProfile : Profile
{
    public UpdateLessonSampleModelProfile()
    {
        CreateMap<UpdateLessonSampleModel, LessonSample>();
    }
}
