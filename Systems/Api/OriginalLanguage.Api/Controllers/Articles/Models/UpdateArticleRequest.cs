using AutoMapper;
using FluentValidation;
using OriginalLanguage.Services.Articles.Models;

namespace OriginalLanguage.Api.Controllers.Articles.Models;

public class UpdateArticleRequest
{
    public Guid AuthorId { get; set; }
    public virtual string? Title { get; set; }
    public virtual string? Content { get; set; }

    public string? ShortDescription { get; set; }
}


public class UpdateArticleRequestValidator : AbstractValidator<UpdateArticleRequest>
{
    public UpdateArticleRequestValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(50).WithMessage("Title is too long.");

        RuleFor(x => x.ShortDescription)
            .MaximumLength(255).WithMessage("Short description is too long.");
    }
}

public class UpdateArticleRequestProfile : Profile
{
    public UpdateArticleRequestProfile()
    {
        CreateMap<UpdateArticleRequest, UpdateArticleModel>();
    }
}
