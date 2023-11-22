using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonProgresses.Models;
public class LessonProgressResponse
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int LessonId { get; set; }
    public int ProgressLevel { get; set; }
}

public class LessonProgressResponseProfile : Profile
{
    public LessonProgressResponseProfile()
    {
        CreateMap<LessonProgressModel, LessonProgressResponse>();
    }
}