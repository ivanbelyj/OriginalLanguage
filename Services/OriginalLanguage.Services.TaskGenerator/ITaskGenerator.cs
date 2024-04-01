using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator;

/// <summary>
/// Generates a set of lesson tasks based on the progress level
/// </summary>
public interface ITaskGenerator
{
    Task<IEnumerable<LessonTask>> GenerateLessonTasks(
        int lessonId,
        int progressLevel);
}
