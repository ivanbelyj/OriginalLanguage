using AutoMapper;
using FluentValidation;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Sentences.Models;
public class UpdateSentenceModel
{
    public string? Text { get; set; }
    public string? Translation { get; set; }

    public int LessonSampleId { get; set; }
}

public class UpdateSentenceModelValidator : AbstractValidator<UpdateSentenceModel>
{
    public UpdateSentenceModelValidator()
    {

    }
}

public class UpdateSentenceModelProfile : Profile
{
    public UpdateSentenceModelProfile()
    {
        CreateMap<UpdateSentenceModel, Sentence>();
    }
}
