using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Articles;
public static class Bootstrapper
{
    public static IServiceCollection AddArticlesService(
        this IServiceCollection services)
    {
        services.AddSingleton<IArticleService, ArticleService>();
        return services;
    }
}
