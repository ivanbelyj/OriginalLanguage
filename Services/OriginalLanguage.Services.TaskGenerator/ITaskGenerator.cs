using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator;
public interface ITaskGenerator
{
    Task<IEnumerable<LessonTask>> GenerateLessonTasks(
        int lessonId,
        int progressLevel);
}
