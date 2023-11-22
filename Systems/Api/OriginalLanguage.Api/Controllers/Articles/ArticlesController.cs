using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Api.Controllers.Articles.Models;
using OriginalLanguage.Common.Responses;
using OriginalLanguage.Services.Articles;
using OriginalLanguage.Services.Articles.Models;

namespace OriginalLanguage.Api.Controllers.Articles;

[ProducesResponseType(typeof(ErrorResponse), 400)]
[Route("api/v{version:apiVersion}/articles")]
[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
public class ArticlesController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ILogger<ArticlesController> logger;
    private readonly IArticleService articleService;
    public ArticlesController(IMapper mapper, ILogger<ArticlesController> logger,
        IArticleService articleService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.articleService = articleService;
    }

    [ProducesResponseType(typeof(IEnumerable<ArticleResponse>), 200)]
    [HttpGet("")]
    public async Task<IEnumerable<ArticleResponse>> GetArticles(
        [FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var articles = await articleService.GetArticles(offset, limit);
        return mapper.Map<IEnumerable<ArticleResponse>>(articles);
    }

    [ProducesResponseType(typeof(ArticleResponse), 200)]
    [HttpGet("{id}")]
    public async Task<ArticleResponse> GetArticle([FromRoute] int id)
    {
        ArticleModel articleModel = await articleService.GetArticle(id);
        ArticleResponse response = mapper.Map<ArticleResponse>(articleModel);
        return response;
    }

    [HttpPost("")]
    public async Task<ArticleResponse> AddArticle([FromBody] AddArticleRequest request)
    {
        AddArticleModel addArticleModel = mapper.Map<AddArticleModel>(request);
        ArticleModel articleModel = await articleService.AddArticle(addArticleModel);
        ArticleResponse response = mapper.Map<ArticleResponse>(articleModel);
        return response;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateArticle([FromRoute] int id,
        [FromBody] UpdateArticleRequest request)
    {
        var model = mapper.Map<UpdateArticleModel>(request);
        await articleService.UpdateArticle(id, model);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticle([FromRoute] int id)
    {
        await articleService.DeleteArticle(id);

        return Ok();
    }

}
