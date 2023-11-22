using AutoMapper;
using FluentValidation;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Lessons.Models;
public class UpdateLessonModel
{
    public int? TheoryArticleId { get; set; }
    public int Number { get; set; }
    public int CourseId { get; set; }
}

public class UpdateLanguageModelValidator : AbstractValidator<UpdateLessonModel>
{
    public UpdateLanguageModelValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("Lesson number is required");

        RuleFor(x => x.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required");
    }
}

public class UpdateLessonModelProfile : Profile
{
    public UpdateLessonModelProfile()
    {
        CreateMap<UpdateLessonModel, Lesson>();
    }
}
