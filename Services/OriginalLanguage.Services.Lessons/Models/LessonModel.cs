using AutoMapper;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Lessons.Models;
public class LessonModel
{
    public int Id { get; set; }
    public int? TheoryArticleId { get; set; }
    public int Number { get; set; }
    public int CourseId { get; set; }
}

public class LessonModelProfile : Profile
{
    public LessonModelProfile()
    {
        CreateMap<Lesson, LessonModel>();
    }
}