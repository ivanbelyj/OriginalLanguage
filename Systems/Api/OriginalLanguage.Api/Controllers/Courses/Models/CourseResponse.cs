using AutoMapper;
using OriginalLanguage.Services.Courses.Models;

namespace OriginalLanguage.Api.Controllers.Courses.Models;

public class CourseResponse
{
    public int Id { get; set; }
    public Guid AuthorId { get; set; }
    public int? LanguageId { get; set; }
    public string? Title { get; set; }
    public DateTime DateTimeAdded { get; set; }
}

public class CourseResponseProfile : Profile
{
    public CourseResponseProfile()
    {
        CreateMap<CourseModel, CourseResponse>();
    }
}