using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonProgresses.Models;
public class AddLessonProgressRequest
{
    public Guid UserId { get; set; }
    public int LessonId { get; set; }
    public int ProgressLevel { get; set; }
}

public class AddLessonProgressRequestValidator : AbstractValidator<AddLessonProgressRequest>
{
    public AddLessonProgressRequestValidator()
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

public class AddLessonProgressRequestProfile : Profile
{
    public AddLessonProgressRequestProfile()
    {
        CreateMap<AddLessonProgressRequest, AddLessonProgressModel>();
    }
}
