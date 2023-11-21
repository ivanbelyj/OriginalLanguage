using AutoMapper;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Articles.Models;
public class ArticleModel
{
    public int Id { get; set; }
    public Guid AuthorId { get; set; }

    public virtual string? Title { get; set; }
    public virtual string? Content { get; set; }

    public DateTime DateTimeAdded { get; set; }
    public string? ShortDescription { get; set; }
}

public class ArticleModelProfile : Profile
{
    public ArticleModelProfile()
    {
        CreateMap<Article, ArticleModel>();
    }
}
