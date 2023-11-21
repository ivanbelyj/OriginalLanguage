using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OriginalLanguage.Common.Exceptions;
using OriginalLanguage.Common.Validator;
using OriginalLanguage.Context;
using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.Articles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Articles;
public class ArticleService : IArticleService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<AddArticleModel> addArticleModelValidator;
    private readonly IModelValidator<UpdateArticleModel> updateArticleModelValidator;
    public ArticleService(IDbContextFactory<MainDbContext> dbContextFactory,
        IMapper mapper,
        IModelValidator<AddArticleModel> addArticleModelValidator,
        IModelValidator<UpdateArticleModel> updateArticleModelValidator) {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.addArticleModelValidator = addArticleModelValidator;
        this.updateArticleModelValidator = updateArticleModelValidator;
    }
    public async Task<ArticleModel> AddArticle(AddArticleModel model)
    {
        addArticleModelValidator.Check(model);
        using var context = await dbContextFactory.CreateDbContextAsync();

        Article article = mapper.Map<Article>(model);
        await context.Articles.AddAsync(article);
        context.SaveChanges();

        return mapper.Map<ArticleModel>(article);
    }

    public async Task<IEnumerable<ArticleModel>> GetArticles(
        int offset = 0, int limit = 10)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        var articles = context.Articles.Include(x => x.Lesson).AsQueryable();
        articles = articles
            .Skip(Math.Max(0, offset))
            .Take(Math.Max(0, Math.Min(limit, 1000)));
        return (await articles.ToListAsync())
            .Select(mapper.Map<ArticleModel>);
    }

    public async Task<ArticleModel> GetArticle(int articleId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        Article? article = await context.Articles
            //.Include(x => x.Lesson)
            .FirstOrDefaultAsync(x => x.Id == articleId);

        ArticleModel articleModel = mapper.Map<ArticleModel>(article);
        return articleModel;
    }

    public async Task DeleteArticle(int articleId)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        Article article = await context.Articles
            .FirstOrDefaultAsync(x => x.Id == articleId) ??
            throw new ProcessException($"The article (id: {articleId}) was not found");

        context.Remove(article);
        context.SaveChanges();
    }

    public async Task UpdateArticle(int articleId, UpdateArticleModel model)
    {
        updateArticleModelValidator.Check(model);

        using var context = await dbContextFactory.CreateDbContextAsync();
        
        Article article = context.Articles.FirstOrDefault(x => x.Id == articleId)
            ?? throw new ProcessException($"The article (id: {articleId}) was not found");

        article = mapper.Map(model, article);

        context.Articles.Update(article);
        context.SaveChanges();
    }
}
