using AutoMapper;
using OriginalLanguage.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonProgresses.Models;
public class LessonProgressModel
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int LessonId { get; set; }
    public int ProgressLevel { get; set; }
}

public class LessonProgressModelProfile : Profile
{
    public LessonProgressModelProfile()
    {
        CreateMap<LessonProgress, LessonProgressModel>();
        CreateMap<LessonProgressModel, UpdateLessonProgressModel>();
    }
}