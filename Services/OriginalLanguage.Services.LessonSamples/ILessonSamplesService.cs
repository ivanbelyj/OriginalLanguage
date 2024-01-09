using OriginalLanguage.Services.LessonSamples.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonSamples;
public interface ILessonSamplesService
{
    Task<IEnumerable<LessonSampleModel>> GetLessonSamples(int offset = 0,
        int limit = 10);
    Task<IEnumerable<LessonSampleModel>> GetSamplesOfLesson(int lessonId);
    Task<LessonSampleModel> GetLessonSample(int id);
    Task<LessonSampleModel> AddLessonSample(AddLessonSampleModel model);
    Task UpdateLessonSample(int id, UpdateLessonSampleModel model);
    Task DeleteLessonSample(int id);
}
