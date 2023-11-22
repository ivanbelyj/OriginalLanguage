using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Api.Controllers.Lessons.Models;
using OriginalLanguage.Common.Responses;
using OriginalLanguage.Services.Lessons;
using OriginalLanguage.Services.Lessons.Models;

namespace OriginalLanguage.Api.Controllers.Lessons;

/// <response code="400">Bad Request</response>
/// <response code="401">Unauthorized</response>
/// <response code="403">Forbidden</response>
/// <response code="404">Not Found</response>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Route("api/v{version:apiVersion}/lessons")]
[ApiController]
[Produces("application/json")]
[ApiVersion("1.0")]
public class LessonsController : ControllerBase
{
    private readonly ILessonsService lessonsService;
    private readonly IMapper mapper;
    public LessonsController(IMapper mapper, ILessonsService lessonsService)
    {
        this.lessonsService = lessonsService;
        this.mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LessonResponse), 200)]
    public async Task<LessonResponse> GetLesson([FromRoute] int id)
    {
        return mapper.Map<LessonResponse>(await lessonsService.GetLesson(id));
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(IEnumerable<LessonResponse>), 200)]
    public async Task<IEnumerable<LessonResponse>> GetLesson(
        [FromQuery] int offset = 0,
        [FromQuery] int limit = 10)
    {
        return mapper.Map<IEnumerable<LessonResponse>>(
            await lessonsService.GetLessons(offset, limit));
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(LessonResponse), 200)]
    public async Task<LessonResponse> AddLesson([FromBody] AddLessonRequest request)
    {
        return mapper.Map<LessonResponse>(
            await lessonsService.AddLesson(mapper.Map<AddLessonModel>(request)));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> UpdateLesson(
        [FromRoute] int id,
        [FromBody] UpdateLessonRequest request)
    {
        await lessonsService.UpdateLesson(id, mapper.Map<UpdateLessonModel>(request));
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(IActionResult), 200)]
    public async Task<IActionResult> DeleteLesson([FromRoute] int id)
    {
        await lessonsService.DeleteLesson(id);
        return Ok();
    }
}
