using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.Courses;
using OriginalLanguage.Services.LessonProgresses;
using OriginalLanguage.Services.Lessons;
using OriginalLanguage.Services.LessonSamples;
using OriginalLanguage.Services.Sentences;

namespace OriginalLanguage.Api.Helpers;

public class ResourceOwningHelper
{
    private readonly ICoursesService coursesService;
    private readonly ILessonsService lessonsService;
    private readonly ILessonSamplesService lessonSamplesService;
    private readonly ISentencesService sentencesService;
    private readonly ILessonProgressesService lessonProgressesService;

    public ResourceOwningHelper(
        ICoursesService coursesService,
        ILessonsService lessonsService,
        ILessonSamplesService lessonSamplesService,
        ISentencesService sentencesService,
        ILessonProgressesService lessonProgressesService)
    {
        this.coursesService = coursesService;
        this.lessonsService = lessonsService;
        this.lessonSamplesService = lessonSamplesService;
        this.sentencesService = sentencesService;
        this.lessonProgressesService = lessonProgressesService;
    }

    public async Task<Guid> GetLessonProgressUserId(int lessonProgressUserId) =>
        (await lessonProgressesService.GetLessonProgress(lessonProgressUserId)).UserId;

    public async Task<Guid> GetSentenceAuthorId(int sentenceId)
    {
        int lessonSampleId = await GetLessonSampleIdBySentenceId(sentenceId);
        return await GetLessonSampleAuthorId(lessonSampleId);
    }

    public async Task<Guid> GetLessonSampleAuthorId(int lessonSampleId)
    {
        int lessonId = await GetLessonIdByLessonSampleId(lessonSampleId);
        return await GetLessonAuthorId(lessonId);
    }

    public async Task<Guid> GetLessonAuthorId(int lessonId)
    {
        int courseId = await GetCourseIdByLessonId(lessonId);
        return await GetCourseAuthorId(courseId);
    }

    public async Task<Guid> GetCourseAuthorId(int courseId) =>
        (await coursesService.GetCourse(courseId)).AuthorId;

    private async Task<int> GetLessonSampleIdBySentenceId(int sentenceId) =>
        (await sentencesService.GetSentence(sentenceId)).LessonSampleId;

    private async Task<int> GetLessonIdByLessonSampleId(int lessonSampleId) =>
        (await lessonSamplesService.GetLessonSample(lessonSampleId)).LessonId;

    private async Task<int> GetCourseIdByLessonId(int lessonSampleId) =>
        (await lessonsService.GetLesson(lessonSampleId)).CourseId;
}
