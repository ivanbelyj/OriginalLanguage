using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Api.Controllers.LessonSamples.Models;
using OriginalLanguage.Common.Responses;
using OriginalLanguage.Services.LessonSamples;
using OriginalLanguage.Services.LessonSamples.Models;

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
public class LessonSamplesController : ControllerBase
{
    private readonly ILessonSamplesService lessonSamplesService;
    private readonly IMapper mapper;
    public LessonSamplesController(IMapper mapper,
        ILessonSamplesService lessonSamplesService)
    {
        this.lessonSamplesService = lessonSamplesService;
        this.mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LessonSampleResponse), 200)]
    public async Task<LessonSampleResponse> GetLessonSample([FromRoute] int id)
    {
        return mapper.Map<LessonSampleResponse>(
            await lessonSamplesService.GetLessonSample(id));
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(IEnumerable<LessonSampleResponse>), 200)]
    public async Task<IEnumerable<LessonSampleResponse>> GetLessonSamples(
        [FromQuery] int offset = 0,
        [FromQuery] int limit = 10)
    {
        return mapper.Map<IEnumerable<LessonSampleResponse>>(
            await lessonSamplesService.GetLessonSamples(offset, limit));
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(LessonSampleResponse), 200)]
    public async Task<LessonSampleResponse> AddLessonSample(
        [FromBody] AddLessonSampleRequest request)
    {
        return mapper.Map<LessonSampleResponse>(
            await lessonSamplesService.AddLessonSample(
                mapper.Map<AddLessonSampleModel>(request)));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLessonSample(
        [FromRoute] int id,
        [FromBody] UpdateLessonSampleRequest request)
    {
        await lessonSamplesService.UpdateLessonSample(id,
            mapper.Map<UpdateLessonSampleModel>(request));

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLessonSample([FromRoute] int id)
    {
        await lessonSamplesService.DeleteLessonSample(id);
        return Ok();
    }
}
