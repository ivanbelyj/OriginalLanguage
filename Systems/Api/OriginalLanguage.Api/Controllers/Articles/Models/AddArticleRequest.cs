using AutoMapper;
using FluentValidation;
using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.Articles.Models;

namespace OriginalLanguage.Api.Controllers.Articles.Models;

public class AddArticleRequest
{
    public virtual string? Title { get; set; }
    public virtual string? Content { get; set; }

    public string? ShortDescription { get; set; }

    public int AuthorId { get; set; }
    //public bool IsLessonTheory { get; set; }
}

public class AddArticleRequestValidator : AbstractValidator<AddArticleRequest>
{
    public AddArticleRequestValidator()
    {
        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("Author is required.");

        RuleFor(x => x.Title)
            .MaximumLength(50).WithMessage("Title is too long.");

        RuleFor(x => x.ShortDescription)
            .MaximumLength(255).WithMessage("Short description is too long.");
    }
}

public class AddArticleRequestProfile : Profile
{
    public AddArticleRequestProfile()
    {
        CreateMap<AddArticleRequest, AddArticleModel>();
    }
}

