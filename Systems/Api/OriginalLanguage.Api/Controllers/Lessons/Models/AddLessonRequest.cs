using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.Lessons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Api.Controllers.Lessons.Models;
public class AddLessonRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? TheoryArticleId { get; set; }
    public int Number { get; set; }
    public int CourseId { get; set; }
}

public class AddLessonRequestValidator : AbstractValidator<AddLessonRequest>
{
    public AddLessonRequestValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("Lesson number is required");

        RuleFor(x => x.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required");
    }
}

public class AddLessonRequestProfile : Profile
{
    public AddLessonRequestProfile()
    {
        CreateMap<AddLessonRequest, AddLessonModel>();
    }
}
