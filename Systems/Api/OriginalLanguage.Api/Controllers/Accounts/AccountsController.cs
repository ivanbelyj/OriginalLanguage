using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Api.Controllers.Articles.Models;
using OriginalLanguage.Api.Controllers.Courses.Models;
using OriginalLanguage.Api.Controllers.Languages.Models;
using OriginalLanguage.Common.Responses;
using OriginalLanguage.Services.Articles;
using OriginalLanguage.Services.Courses;
using OriginalLanguage.Services.Languages;
using OriginalLanguage.Services.UserAccount;
using OriginalLanguage.Services.UserAccount.Models;

namespace OriginalLanguage.Api.Controllers.Accounts;

/// <response code="400">Bad Request</response>
/// <response code="401">Unauthorized</response>
/// <response code="403">Forbidden</response>
/// <response code="404">Not Found</response>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/accounts")]
[ApiController]
[ApiVersion("1.0")]
public class AccountsController : ControllerBase
{
    private readonly IUserAccountService userAccountService;
    private readonly ICoursesService coursesService;
    private readonly ILanguagesService languagesService;
    private readonly IArticleService articleService;
    private readonly IMapper mapper;
    public AccountsController(IMapper mapper, 
        IUserAccountService userAccountService,
        ICoursesService coursesService,
        ILanguagesService languagesService,
        IArticleService articleService)
    {
        this.mapper = mapper;
        this.userAccountService = userAccountService;
        this.coursesService = coursesService;
        this.languagesService = languagesService;
        this.articleService = articleService;
    }

    [HttpPost("")]
    public async Task<UserAccountResponse> Register(
        [FromBody] RegisterAccountRequest request)
    {
        UserAccountModel userAccountModel = await userAccountService
            .CreateUser(mapper.Map<RegisterUserAccountModel>(request));
        var response = mapper.Map<UserAccountResponse>(userAccountModel);
        return response;
    }

    [HttpGet("{authorId}/courses")]
    public async Task<IEnumerable<CourseResponse>> GetUserCourses(
        [FromRoute] Guid authorId)
    {
        var courses = await coursesService.GetUserCourses(authorId);
        return mapper.Map<IEnumerable<CourseResponse>>(courses);
    }

    [HttpGet("{authorId}/languages")]
    public async Task<IEnumerable<LanguageResponse>> GetUserLanguages(
        [FromRoute] Guid authorId)
    {
        var courses = await languagesService.GetUserLanguages(authorId);
        return mapper.Map<IEnumerable<LanguageResponse>>(courses);
    }

    [HttpGet("{authorId}/articles")]
    public async Task<IEnumerable<ArticleResponse>> GetUserArticles(
        [FromRoute] Guid authorId)
    {
        var articles = await articleService.GetUserArticles(authorId);
        return mapper.Map<IEnumerable<ArticleResponse>>(articles);
    }
}
