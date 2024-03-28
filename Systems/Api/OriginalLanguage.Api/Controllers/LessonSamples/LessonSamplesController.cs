using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Api.Controllers.LessonSamples.Models;
using OriginalLanguage.Api.Controllers.Sentences.Models;
using OriginalLanguage.Api.Helpers;
using OriginalLanguage.Common.Responses;
using OriginalLanguage.Consts;
using OriginalLanguage.Services.Courses;
using OriginalLanguage.Services.Lessons;
using OriginalLanguage.Services.LessonSamples;
using OriginalLanguage.Services.LessonSamples.Models;
using OriginalLanguage.Services.Sentences;

namespace OriginalLanguage.Api.Controllers.LessonSamples;

/// <response code="400">Bad Request</response>
/// <response code="401">Unauthorized</response>
/// <response code="403">Forbidden</response>
/// <response code="404">Not Found</response>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Route("api/v{version:apiVersion}/lesson-samples")]
[ApiController]
[Produces("application/json")]
[ApiVersion("1.0")]
public class LessonSamplesController : AppControllerBase
{
    private readonly IMapper mapper;
    private readonly ILessonSamplesService lessonSamplesService;
    private readonly ISentencesService sentencesService;
    private readonly ResourceOwningHelper resourceOwningHelper;
    
    public LessonSamplesController(
        IMapper mapper,
        ILessonSamplesService lessonSamplesService,
        ISentencesService sentencesService,
        IAuthorizationService authorizationService,
        ResourceOwningHelper resourceOwningHelper)
        : base (authorizationService)
    {
        this.mapper = mapper;
        this.lessonSamplesService = lessonSamplesService;
        this.sentencesService = sentencesService;
        this.resourceOwningHelper = resourceOwningHelper;
    }

    [ProducesResponseType(typeof(LessonSampleResponse), 200)]
    [Authorize(AppScopes.CoursesLearn)]
    [HttpGet("{id}")]
    public async Task<LessonSampleResponse> GetLessonSample([FromRoute] int id)
    {
        return mapper.Map<LessonSampleResponse>(
            await lessonSamplesService.GetLessonSample(id));
    }

    //[HttpGet("")]
    //[ProducesResponseType(typeof(IEnumerable<LessonSampleResponse>), 200)]
    //public async Task<IEnumerable<LessonSampleResponse>> GetLessonSamples(
    //    [FromQuery] int offset = 0,
    //    [FromQuery] int limit = 10)
    //{
    //    return mapper.Map<IEnumerable<LessonSampleResponse>>(
    //        await lessonSamplesService.GetLessonSamples(offset, limit));
    //}

    [ProducesResponseType(typeof(IEnumerable<SentenceResponse>), 200)]
    [Authorize(AppScopes.CoursesLearn)]
    [HttpGet("{lessonSampleId}/sentences")]
    public async Task<IEnumerable<SentenceResponse>> GetLessonSampleSentences(
        [FromRoute] int lessonSampleId)
    {
        var sentences = await sentencesService
            .GetLessonSampleSentences(lessonSampleId);
        return mapper.Map<IEnumerable<SentenceResponse>>(sentences);
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(LessonSampleResponse), 200)]
    public async Task<IActionResult> AddLessonSample(
        [FromBody] AddLessonSampleRequest request)
    {
        string lessonOwnerId = (await resourceOwningHelper
            .GetLessonAuthorId(request.LessonId))
            .ToString();
        var res = await ForbidIfResourceIsNotOwned(lessonOwnerId);
        if (res != null) return res;

        var addLessonSampleModel = mapper.Map<AddLessonSampleModel>(request);
        var addedLessonSampeModel = await lessonSamplesService
            .AddLessonSample(addLessonSampleModel);
        return Ok(mapper.Map<LessonSampleResponse>(addedLessonSampeModel));
    }

    [Authorize(AppScopes.ContentWrite)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLessonSample(
        [FromRoute] int id,
        [FromBody] UpdateLessonSampleRequest request)
    {
        var res = await ForbidExistingNotOwnedLessonSample(id);
        if (res != null)
            return res;

        var updateLessonSampleModel = mapper.Map<UpdateLessonSampleModel>(request);
        await lessonSamplesService.UpdateLessonSample(id, updateLessonSampleModel);

        return Ok();
    }

    [Authorize(AppScopes.ContentWrite)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLessonSample([FromRoute] int id)
    {
        var res = await ForbidExistingNotOwnedLessonSample(id);
        if (res != null)
            return res;

        await lessonSamplesService.DeleteLessonSample(id);
        return Ok();
    }

    private async Task<IActionResult?> ForbidExistingNotOwnedLessonSample(
        int lessonSampleId)
    {
        string ownerId = (await resourceOwningHelper
            .GetLessonSampleAuthorId(lessonSampleId))
            .ToString();

        return await ForbidIfResourceIsNotOwned(ownerId);
    }
}
