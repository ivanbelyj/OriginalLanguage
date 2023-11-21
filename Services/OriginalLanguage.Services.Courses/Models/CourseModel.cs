using AutoMapper;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Courses.Models;
public class CourseModel
{
    public int Id { get; set; }
    public Guid AuthorId { get; set; }
    public int? LanguageId { get; set; }
    public string? Title { get; set; }
    public DateTime DateTimeAdded { get; set; }
}

public class CourseModelProfile : Profile
{
    public CourseModelProfile()
    {
        CreateMap<Course, CourseModel>();
    }
}