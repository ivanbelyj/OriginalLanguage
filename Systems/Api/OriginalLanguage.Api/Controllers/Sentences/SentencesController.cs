using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Api.Controllers.Sentences.Models;
using OriginalLanguage.Api.Helpers;
using OriginalLanguage.Common.Responses;
using OriginalLanguage.Consts;
using OriginalLanguage.Services.Sentences;
using OriginalLanguage.Services.Sentences.Models;

namespace OriginalLanguage.Api.Controllers.Sentences;

/// <response code="400">Bad Request</response>
/// <response code="401">Unauthorized</response>
/// <response code="403">Forbidden</response>
/// <response code="404">Not Found</response>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[ApiController]
[Produces("application/json")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/sentences")]
public class SentencesController : AppControllerBase
{
    private readonly IMapper mapper;
    private readonly ISentencesService sentencesService;
    private readonly ResourceOwningHelper resourceOwningHelper;

    public SentencesController(
        IMapper mapper,
        ISentencesService sentencesService,
        ResourceOwningHelper resourceOwningHelper,
        IAuthorizationService authorizationService)
        : base (authorizationService)
    {
        this.mapper = mapper;
        this.sentencesService = sentencesService;
        this.resourceOwningHelper = resourceOwningHelper;
    }

    //[ProducesResponseType(typeof(IEnumerable<SentenceResponse>), 200)]
    //[HttpGet("")]
    //public async Task<IEnumerable<SentenceResponse>> GetSentences(
    //    [FromQuery] int offset = 0,
    //    [FromQuery] int limit = 10)
    //{
    //    return mapper.Map<IEnumerable<SentenceResponse>>(
    //        await sentencesService.GetSentences(offset, limit));
    //}

    [ProducesResponseType(typeof(SentenceResponse), 200)]
    [Authorize(AppScopes.CoursesLearn)]
    [HttpGet("{id}")]
    public async Task<SentenceResponse> GetSentence(
        [FromRoute] int id)
    {
        return mapper.Map<SentenceResponse>(await sentencesService.GetSentence(id));
    }

    [ProducesResponseType(typeof(SentenceResponse), 200)]
    [Authorize(AppScopes.ContentWrite)]
    [HttpPost("")]
    public async Task<IActionResult> AddSentence(
        [FromBody] AddSentenceRequest request)
    {
        string lessonSampleAuthorId = (await resourceOwningHelper
            .GetLessonSampleAuthorId(request.LessonSampleId))
            .ToString();
        var res = await ForbidIfResourceIsNotOwned(lessonSampleAuthorId);
        if (res != null) return res;

        var addSentenceModel = mapper.Map<AddSentenceModel>(request);
        var addedSentenceModel = await sentencesService.AddSentence(addSentenceModel);
        return Ok(mapper.Map<SentenceResponse>(addedSentenceModel));
    }

    [Authorize(AppScopes.ContentWrite)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSentence(
        [FromRoute] int id,
        [FromBody] UpdateSentenceRequest request)
    {
        var res = await ForbidExistingNotOwnedSentence(id);
        if (res != null) return res;

        await sentencesService.UpdateSentence(id,
            mapper.Map<UpdateSentenceModel>(request));

        return Ok();
    }

    [Authorize(AppScopes.ContentWrite)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSentence([FromRoute] int id)
    {
        var res = await ForbidExistingNotOwnedSentence(id);
        if (res != null) return res;

        await sentencesService.DeleteSentence(id);

        return Ok();
    }

    private async Task<IActionResult?> ForbidExistingNotOwnedSentence(
        int sentenceId)
    {
        string ownerId = (await resourceOwningHelper
            .GetSentenceAuthorId(sentenceId))
            .ToString();

        return await ForbidIfResourceIsNotOwned(ownerId);
    }
}
