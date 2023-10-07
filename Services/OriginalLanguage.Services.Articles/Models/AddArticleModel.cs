using AutoMapper;
using FluentValidation;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Articles.Models;
public class AddArticleModel
{
    public virtual string? Title { get; set; }
    public virtual string? Content { get; set; }

    public string? ShortDescription { get; set; }

    public int AuthorId { get; set; }
    //public bool IsLessonTheory { get; set; }
}

public class AddArticleModelValidator : AbstractValidator<AddArticleModel>
{
    public AddArticleModelValidator() {
        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("Author is required.");

        RuleFor(x => x.Title)
            .MaximumLength(50).WithMessage("Title is too long.");

        RuleFor(x => x.ShortDescription)
            .MaximumLength(255).WithMessage("Short description is too long.");
    }
}

public class AddArticleModelProfile : Profile
{
    public AddArticleModelProfile()
    {
        CreateMap<AddArticleModel, Article>();
    }
}
