using AutoMapper;
using OriginalLanguage.Services.Courses.Models;

namespace OriginalLanguage.Api.Controllers.Courses.Models;

public class AddCourseRequest
{
    public Guid AuthorId { get; set; }
    public int? LanguageId { get; set; }
    public string? Title { get; set; }
}

public class AddCourseRequestProfile : Profile
{
    public AddCourseRequestProfile()
    {
        CreateMap<AddCourseRequest, AddCourseModel>();
    }
}