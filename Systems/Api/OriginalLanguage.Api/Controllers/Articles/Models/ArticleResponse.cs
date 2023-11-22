using AutoMapper;
using OriginalLanguage.Services.Articles.Models;

namespace OriginalLanguage.Api.Controllers.Articles.Models;

public class ArticleResponse
{
    public int Id { get; set; }
    public Guid AuthorId { get; set; }
    public virtual string? Title { get; set; }
    public virtual string? Content { get; set; }

    public DateTime DateTimeAdded { get; set; }
    public string? ShortDescription { get; set; }
}

public class ArticleResponseProfile : Profile
{
    public ArticleResponseProfile()
    {
        CreateMap<ArticleModel, ArticleResponse>();
    }
}
