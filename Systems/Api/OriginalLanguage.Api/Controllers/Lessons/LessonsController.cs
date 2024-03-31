using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OriginalLanguage.Api.Controllers.Lessons.Models;
using OriginalLanguage.Api.Controllers.LessonSamples.Models;
using OriginalLanguage.Common.Responses;
using OriginalLanguage.Services.Lessons;
using OriginalLanguage.Services.Lessons.Models;
using OriginalLanguage.Services.LessonSamples;
using OriginalLanguage.Services.TaskGenerator;
using OriginalLanguage.Services.TaskAnswerChecker.Models;
using OriginalLanguage.Services.TaskAnswerChecker;
using OriginalLanguage.Services.LessonCompletion;
using OriginalLanguage.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using OriginalLanguage.Consts;
using OriginalLanguage.Context.Entities;

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
public class LessonsController : AppControllerBase
{
    private readonly IMapper mapper;
    private readonly ILessonsService lessonsService;
    private readonly ILessonSamplesService lessonSamplesService;
    private readonly ITaskGenerator taskGenerator;
    private readonly ITaskAnswerChecker taskAnswerChecker;
    private readonly ILessonCompletionService lessonCompletionService;
    private readonly ResourceOwningHelper resourceOwningHelper;

    public LessonsController(
        IMapper mapper,
        ILessonsService lessonsService,
        ILessonSamplesService lessonSamplesService,
        ITaskGenerator taskGenerator,
        ITaskAnswerChecker taskAnswerChecker,
        ILessonCompletionService lessonCompletionService,
        ResourceOwningHelper resourceOwningHelper,
        IAuthorizationService authorizationService)
        : base(authorizationService)
    {
        this.lessonsService = lessonsService;
        this.lessonSamplesService = lessonSamplesService;
        this.taskGenerator = taskGenerator;
        this.taskAnswerChecker = taskAnswerChecker;
        this.lessonCompletionService = lessonCompletionService;
        this.resourceOwningHelper = resourceOwningHelper;
        this.mapper = mapper;
    }

    [ProducesResponseType(typeof(LessonResponse), 200)]
    [HttpGet("{id}")]
    public async Task<LessonResponse> GetLesson([FromRoute] int id)
    {
        return mapper.Map<LessonResponse>(await lessonsService.GetLesson(id));
    }

    //[ProducesResponseType(typeof(IEnumerable<LessonResponse>), 200)]
    //[HttpGet("")]
    //public async Task<IEnumerable<LessonResponse>> GetLessons(
    //    [FromQuery] int offset = 0,
    //    [FromQuery] int limit = 10)
    //{
    //    return mapper.Map<IEnumerable<LessonResponse>>(
    //        await lessonsService.GetLessons(offset, limit));
    //}

    [ProducesResponseType(typeof(IEnumerable<LessonSampleResponse>), 200)]
    [Authorize(AppScopes.CoursesLearn)]
    [HttpGet("{lessonId}/lesson-samples")]
    public async Task<IEnumerable<LessonSampleResponse>> GetSamplesOfLesson(
        [FromRoute] int lessonId)
    {
        var lessonSamples = await lessonSamplesService
            .GetSamplesOfLesson(lessonId);
        return mapper.Map<IEnumerable<LessonSampleResponse>>(lessonSamples);
    }

    [ProducesResponseType(typeof(LessonResponse), 200)]
    [Authorize(AppScopes.ContentWrite)]
    [HttpPost("")]
    public async Task<IActionResult> AddLesson(
        [FromBody] AddLessonRequest request)
    {
        string ownerId = (await resourceOwningHelper
            .GetCourseAuthorId(request.CourseId))
            .ToString();
        var res = await ForbidIfResourceIsNotOwned(ownerId);
        if (res != null) return res;

        var addedLessonModel = await lessonsService
            .AddLesson(mapper.Map<AddLessonModel>(request));
        return Ok(mapper.Map<LessonResponse>(addedLessonModel));
    }

    [Authorize(AppScopes.ContentWrite)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLesson(
        [FromRoute] int id,
        [FromBody] UpdateLessonRequest request)
    {
        var res = await ForbidExistingNotOwnedLesson(id);
        if (res != null) return res;

        await lessonsService.UpdateLesson(id, mapper.Map<UpdateLessonModel>(request));
        return Ok();
    }

    [Authorize(AppScopes.ContentWrite)]
    [HttpPut("numbers")]
    public async Task<IActionResult> UpdateLessonNumber(
        [FromBody] IEnumerable<LessonIdAndNumber> lessonIdsAndNumbers)
    {
        foreach (var idAndNumber in lessonIdsAndNumbers) {
            var res = await ForbidExistingNotOwnedLesson(idAndNumber.Id);
            if (res != null) return res;
        }

        await lessonsService.UpdateLessonNumbers(lessonIdsAndNumbers);
        return Ok();
    }

    [Authorize(AppScopes.ContentWrite)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLesson([FromRoute] int id)
    {
        var res = await ForbidExistingNotOwnedLesson(id);
        if (res != null) return res;

        await lessonsService.DeleteLesson(id);
        return Ok();
    }

    [ProducesResponseType(typeof(IEnumerable<LessonTask>), 200)]
    [Authorize(AppScopes.CoursesLearn)]
    [HttpGet("{id}/generate-tasks")]
    public async Task<IEnumerable<LessonTask>> GenerateLessons(
        [FromRoute] int id)
    {
        // Todo: get user`s progress level and pass it
        return await taskGenerator.GenerateLessonTasks(id, 0);
    }

    [ProducesResponseType(typeof(CheckAnswerResult), 200)]
    [Authorize(AppScopes.CoursesLearn)]
    [HttpPost("check-task-answer")]
    public async Task<CheckAnswerResult> CheckTaskAnswer(
        [FromBody] TaskAnswer answer)
    {
        return await taskAnswerChecker.Check(answer);
    }

    [ProducesResponseType(typeof(LessonCompletionResult), 200)]
    [Authorize(AppScopes.CoursesLearn)]
    [HttpPost("{id}/complete")]
    public async Task<IActionResult> CheckTaskAnswer(
        [FromRoute] int id,
        [FromBody] IEnumerable<TaskAnswer> answers)
    {
        Guid? userId = GetUserId();

        if (userId == null)
            return Challenge();

        var res = await lessonCompletionService.TryCompleteLesson(
            userId.Value,
            id,
            answers);
        return Ok(res);
    }

    private async Task<IActionResult?> ForbidExistingNotOwnedLesson(int lessonId)
    {
        string resourceOwnerId = (await resourceOwningHelper
            .GetLessonAuthorId(lessonId))
            .ToString();
        return await ForbidIfResourceIsNotOwned(resourceOwnerId);
    }
}
