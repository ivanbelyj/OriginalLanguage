using AutoMapper;
using FluentValidation;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Languages.Models;
public class UpdateLanguageModel
{
    public Guid AuthorId { get; set; }

    public string Name { get; set; }
    public string? NativeName { get; set; }

    public string? About { get; set; }
    public string? AboutNativeSpeakers { get; set; }
    public string? Links { get; set; }

    public string? FlagUrl { get; set; }

    public ConlangDataModel? ConlangData { get; set; }
}

public class UpdateLanguageModelValidator : AbstractValidator<UpdateLanguageModel>
{
    public UpdateLanguageModelValidator()
    {
        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("Author is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Language name is too long.");

        RuleFor(x => x.NativeName)
            .MaximumLength(50)
            .WithMessage("Language native name is too long.");
    }
}

public class UpdateLanguageModelProfile : Profile
{
    public UpdateLanguageModelProfile()
    {
        CreateMap<UpdateLanguageModel, Language>();
    }
}