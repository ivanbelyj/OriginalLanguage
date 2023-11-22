using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonProgresses.Models;
public class AddLessonProgressModel
{
    public Guid UserId { get; set; }
    public int LessonId { get; set; }
    public int ProgressLevel { get; set; }
}

public class AddLessonProgressModelValidator : AbstractValidator<AddLessonProgressModel>
{
    public AddLessonProgressModelValidator()
    {
        RuleFor(x => x.LessonId)
            .NotEmpty()
            .WithMessage("LessonId is required");

        RuleFor(x => x.ProgressLevel)
            .NotEmpty()
            .WithMessage("ProgressLevel is required");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required");
    }
}

public class AddLessonProgressModelProfile : Profile
{
    public AddLessonProgressModelProfile()
    {
        CreateMap<AddLessonProgressModel, LessonProgress>();
    }
}
