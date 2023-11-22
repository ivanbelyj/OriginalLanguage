using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Common.Responses;
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
public class LessonProgressesController : ControllerBase
{
    private readonly ILessonProgressesService lessonProgressesService;
    private readonly IMapper mapper;
    public LessonProgressesController(IMapper mapper,
        ILessonProgressesService lessonsService)
    {
        this.lessonProgressesService = lessonsService;
        this.mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LessonProgressResponse), 200)]
    public async Task<LessonProgressResponse> GetLessonProgress([FromRoute] int id)
    {
        return mapper.Map<LessonProgressResponse>(await lessonProgressesService.GetLessonProgress(id));
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(IEnumerable<LessonProgressResponse>), 200)]
    public async Task<IEnumerable<LessonProgressResponse>> GetLessonProgress(
        [FromQuery] int offset = 0,
        [FromQuery] int limit = 10)
    {
        return mapper.Map<IEnumerable<LessonProgressResponse>>(
            await lessonProgressesService.GetLessonProgresses(offset, limit));
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(LessonProgressResponse), 200)]
    public async Task<LessonProgressResponse> AddLessonProgress(
        [FromBody] AddLessonProgressRequest request)
    {
        return mapper.Map<LessonProgressResponse>(
            await lessonProgressesService.AddLessonProgress(
                mapper.Map<AddLessonProgressModel>(request)));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> UpdateLessonProgress(
        [FromRoute] int id,
        [FromBody] UpdateLessonProgressRequest request)
    {
        await lessonProgressesService.UpdateLessonProgress(id,
            mapper.Map<UpdateLessonProgressModel>(request));
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> DeleteLessonProgress([FromRoute] int id)
    {
        await lessonProgressesService.DeleteLessonProgress(id);
        return Ok();
    }
}
