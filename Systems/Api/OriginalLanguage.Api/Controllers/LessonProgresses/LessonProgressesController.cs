using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Api.Helpers;
using OriginalLanguage.Common.Responses;
using OriginalLanguage.Consts;
using OriginalLanguage.Services.LessonProgresses;
using OriginalLanguage.Services.LessonProgresses.Models;
using System;

namespace OriginalLanguage.Api.Controllers.LessonProgresses;

/// <response code="400">Bad Request</response>
/// <response code="401">Unauthorized</response>
/// <response code="403">Forbidden</response>
/// <response code="404">Not Found</response>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Route("api/v{version:apiVersion}/lesson-progresses")]
[ApiController]
[Produces("application/json")]
[ApiVersion("1.0")]
public class LessonProgressesController : AppControllerBase
{
    private readonly IMapper mapper;
    private readonly ILessonProgressesService lessonProgressesService;
    private readonly ResourceOwningHelper resourceOwningHelper;

    public LessonProgressesController(IMapper mapper,
        ILessonProgressesService lessonProgressesService,
        IAuthorizationService authorizationService,
        ResourceOwningHelper resourceOwningHelper)
        : base(authorizationService)
    {
        this.lessonProgressesService = lessonProgressesService;
        this.resourceOwningHelper = resourceOwningHelper;
        this.mapper = mapper;
    }

    //[HttpGet("{id}")]
    //[ProducesResponseType(typeof(LessonProgressResponse), 200)]
    //public async Task<LessonProgressResponse> GetLessonProgress([FromRoute] int id)
    //{
    //    return mapper.Map<LessonProgressResponse>(await lessonProgressesService.GetLessonProgress(id));
    //}

    //[HttpGet("")]
    //[ProducesResponseType(typeof(IEnumerable<LessonProgressResponse>), 200)]
    //public async Task<IEnumerable<LessonProgressResponse>> GetLessonProgress(
    //    [FromQuery] int offset = 0,
    //    [FromQuery] int limit = 10)
    //{
    //    return mapper.Map<IEnumerable<LessonProgressResponse>>(
    //        await lessonProgressesService.GetLessonProgresses(offset, limit));
    //}

    
    [ProducesResponseType(typeof(LessonProgressResponse), 200)]
    [Authorize(AppScopes.CoursesLearn)]
    [HttpPost("")]
    public async Task<IActionResult> AddLessonProgress(
        [FromBody] AddLessonProgressRequest request)
    {
        var res = await ForbidIfResourceIsNotOwned(request.UserId.ToString());
        if (res != null)
            return res;

        var addLessonProgressModel = mapper.Map<AddLessonProgressModel>(request);
        var addedLessonProgressModel = await lessonProgressesService
            .AddLessonProgress(addLessonProgressModel);

        return Ok(mapper.Map<LessonProgressResponse>(addedLessonProgressModel));
    }

    [ProducesResponseType(typeof(IActionResult), 200)]
    [Authorize(AppScopes.CoursesLearn)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLessonProgress(
        [FromRoute] int id,
        [FromBody] UpdateLessonProgressRequest request)
    {
        var res = await ForbidExistingNotOwnedLessonProgress(id);
        if (res != null) return res;

        var updateProgressModel = mapper.Map<UpdateLessonProgressModel>(request);
        await lessonProgressesService.UpdateLessonProgress(id, updateProgressModel);
        return Ok();
    }

    [ProducesResponseType(typeof(IActionResult), 200)]
    [Authorize(AppScopes.CoursesLearn)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLessonProgress([FromRoute] int id)
    {
        var res = await ForbidExistingNotOwnedLessonProgress(id);
        if (res != null) return res;

        await lessonProgressesService.DeleteLessonProgress(id);
        return Ok();
    }

    private async Task<IActionResult?> ForbidExistingNotOwnedLessonProgress(
        int lessonProgressId)
    {
        string ownerId = (await resourceOwningHelper
            .GetLessonProgressUserId(lessonProgressId))
            .ToString();

        return await ForbidIfResourceIsNotOwned(ownerId);
    }
}
