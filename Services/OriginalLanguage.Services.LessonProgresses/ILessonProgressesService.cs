using OriginalLanguage.Services.LessonProgresses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonProgresses;
public interface ILessonProgressesService
{
    Task<IEnumerable<LessonProgressModel>> GetLessonProgresses(int offset = 0,
        int limit = 10);
    Task<LessonProgressModel> GetLessonProgress(int id);
    Task<LessonProgressModel?> TryGetByUserAndLessonIds(Guid userId, int lessonId);
    Task<LessonProgressModel> AddLessonProgress(AddLessonProgressModel model);
    Task UpdateLessonProgress(int id, UpdateLessonProgressModel model);
    Task DeleteLessonProgress(int id);
    Task IncrementLessonProgress(int id);
}
