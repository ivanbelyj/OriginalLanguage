using OriginalLanguage.Services.TaskAnswerChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonCompletion;
public interface ILessonCompletionService
{
    Task<LessonCompletionResult> TryCompleteLesson(
        Guid userId,
        int lessonId,
        IEnumerable<TaskAnswer> answers);
}
