using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Api.Controllers.Courses.Models;
using OriginalLanguage.Common.Responses;
using OriginalLanguage.Services.Courses;
using OriginalLanguage.Services.Courses.Models;

namespace OriginalLanguage.Api.Controllers.Courses;

/// <response code="400">Bad Request</response>
/// <response code="401">Unauthorized</response>
/// <response code="403">Forbidden</response>
/// <response code="404">Not Found</response>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/courses")]
[ApiController]
[ApiVersion("1.0")]
public class CoursesController : ControllerBase
{
    private readonly ICoursesService coursesService;
    private readonly IMapper mapper;
    public CoursesController(ICoursesService coursesService, IMapper mapper)
    {
        this.coursesService = coursesService;
        this.mapper = mapper;
    }

    [ProducesResponseType(typeof(CourseResponse), 200)]
    [HttpGet("{id}")]
    public async Task<CourseResponse> GetCourse([FromRoute] int id)
    {
        var course = await coursesService.GetCourse(id);
        return mapper.Map<CourseResponse>(course);
    }

    [ProducesResponseType(typeof(IEnumerable<CourseResponse>), 200)]
    [HttpGet("")]
    public async Task<IEnumerable<CourseResponse>> GetCourses(
        [FromQuery] int offset = 0,
        [FromQuery] int limit = 10)
    {
        var courses = await coursesService.GetCourses(offset, limit);
        return mapper.Map<IEnumerable<CourseResponse>>(courses);
    }

    [ProducesResponseType(typeof(CourseResponse), 200)]
    [HttpPost("")]
    public async Task<CourseResponse> AddCourse(
        [FromBody] AddCourseRequest request)
    {
        var model = await coursesService
            .AddCourse(mapper.Map<AddCourseModel>(request));
        return mapper.Map<CourseResponse>(model);
    }

    [ProducesResponseType(200)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(
        [FromRoute] int id,
        [FromBody] UpdateCourseRequest request)
    {
        await coursesService.UpdateCourse(id, mapper.Map<UpdateCourseModel>(request));
        return Ok();
    }

    [ProducesResponseType(200)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse([FromRoute] int id)
    {
        await coursesService.DeleteCourse(id);
        return Ok();
    }
}
