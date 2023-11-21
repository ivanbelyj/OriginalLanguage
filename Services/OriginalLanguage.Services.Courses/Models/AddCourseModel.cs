using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Courses.Models;
public class AddCourseModel
{
    public Guid AuthorId { get; set; }
    public int? LanguageId { get; set; }
    public string? Title { get; set; }
}

public class AddCourseModelValidator : AbstractValidator<AddCourseModel>
{
    public AddCourseModelValidator()
    {
        RuleFor(x => x.AuthorId)
            .NotEmpty()
            .WithMessage("Author is required");
        RuleFor(x => x.Title)
            .MaximumLength(50)
            .WithMessage("Title is too long");
    }
}

public class AddCourseModelProfile : Profile
{
    public AddCourseModelProfile()
    {
        CreateMap<AddCourseModel, Course>();
    }
}
