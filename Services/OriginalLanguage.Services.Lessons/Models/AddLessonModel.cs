using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Lessons.Models;
public class AddLessonModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? TheoryArticleId { get; set; }
    public int Number { get; set; }
    public int CourseId { get; set; }
}

public class AddLessonModelValidator : AbstractValidator<AddLessonModel>
{
    public AddLessonModelValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("Lesson number is required");

        RuleFor(x => x.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required");
    }
}

public class AddLessonModelProfile : Profile
{
    public AddLessonModelProfile()
    {
        CreateMap<AddLessonModel, Lesson>();
    }
}
