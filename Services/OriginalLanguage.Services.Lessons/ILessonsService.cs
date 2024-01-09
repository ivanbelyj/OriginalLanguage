using OriginalLanguage.Services.Lessons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.Lessons;
public interface ILessonsService
{
    Task<IEnumerable<LessonModel>> GetLessons(int offset = 0, int limit = 10);
    Task<IEnumerable<LessonModel>> GetCourseLessons(int courseId);
    Task<LessonModel> GetLesson(int id);
    Task<LessonModel> AddLesson(AddLessonModel model);
    Task UpdateLesson(int id, UpdateLessonModel model);
    Task DeleteLesson(int id);
}
