using OriginalLanguage.Services.Articles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Articles;
public interface IArticleService
{
    Task<IEnumerable<ArticleModel>> GetArticles(int offset = 0, int limit = 10);
    Task<ArticleModel> GetArticle(int articleId);
    Task<ArticleModel> AddArticle(AddArticleModel model);
    Task UpdateArticle(int articleId, UpdateArticleModel model);
    Task DeleteArticle(int articleId);
}
