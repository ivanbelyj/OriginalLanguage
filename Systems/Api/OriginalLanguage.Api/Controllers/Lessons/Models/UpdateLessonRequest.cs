using AutoMapper;
using FluentValidation;
using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.Lessons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Api.Controllers.Lessons.Models;
public class UpdateLessonRequest
{
    public int? TheoryArticleId { get; set; }
    public int Number { get; set; }
    public int CourseId { get; set; }
}

public class UpdateLessonRequestValidator : AbstractValidator<UpdateLessonRequest>
{
    public UpdateLessonRequestValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("Lesson number is required");

        RuleFor(x => x.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required");
    }
}

public class UpdateLessonRequestProfile : Profile
{
    public UpdateLessonRequestProfile()
    {
        CreateMap<UpdateLessonRequest, UpdateLessonModel>();
    }
}
