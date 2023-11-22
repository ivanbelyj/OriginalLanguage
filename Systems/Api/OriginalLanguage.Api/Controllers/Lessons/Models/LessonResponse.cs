using AutoMapper;
using OriginalLanguage.Services.Lessons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Api.Controllers.Lessons.Models;
public class LessonResponse
{
    public int Id { get; set; }
    public int? TheoryArticleId { get; set; }
    public int Number { get; set; }
    public int CourseId { get; set; }
}

public class LessonResponseProfile : Profile
{
    public LessonResponseProfile()
    {
        CreateMap<LessonModel, LessonResponse>();
    }
}
