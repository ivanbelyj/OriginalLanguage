using AutoMapper;
using FluentValidation;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Courses.Models;
public class UpdateCourseModel
{
    public Guid AuthorId { get; set; }
    public int? LanguageId { get; set; }
    public string? Title { get; set; }
}

public class UpdateCourseModelValidator : AbstractValidator<UpdateCourseModel>
{
    public UpdateCourseModelValidator()
    {
        RuleFor(x => x.AuthorId)
            .NotEmpty()
            .WithMessage("Author is required");
        RuleFor(x => x.Title)
            .MaximumLength(50)
            .WithMessage("Title is too long");
    }
}

class UpdateCourseModelProfile : Profile
{
    public UpdateCourseModelProfile()
    {
        CreateMap<UpdateCourseModel, Course>();
    }
}