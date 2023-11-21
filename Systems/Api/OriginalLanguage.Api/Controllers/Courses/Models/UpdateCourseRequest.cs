using AutoMapper;
using OriginalLanguage.Services.Courses.Models;

namespace OriginalLanguage.Api.Controllers.Courses.Models;

public class UpdateCourseRequest
{
    public Guid AuthorId { get; set; }
    public int? LanguageId { get; set; }
    public string? Title { get; set; }
}

public class UpdateCourseRequestProfile : Profile
{
    public UpdateCourseRequestProfile()
    {
        CreateMap<UpdateCourseRequest, UpdateCourseModel>();
    }
}