using AutoMapper;
using FluentValidation;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Articles.Models;
public class UpdateArticleModel
{
    public Guid AuthorId { get; set; }

    public virtual string? Title { get; set; }
    public virtual string? Content { get; set; }

    public string? ShortDescription { get; set; }
}

public class UpdateArticleModelValidator : AbstractValidator<UpdateArticleModel>
{
    public UpdateArticleModelValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(50).WithMessage("Title is too long.");

        RuleFor(x => x.ShortDescription)
            .MaximumLength(255).WithMessage("Short description is too long.");
    }
}

public class UpdateArticleModelProfile : Profile
{
    public UpdateArticleModelProfile()
    {
        CreateMap<UpdateArticleModel, Article>();
    }
}
