using OriginalLanguage.Services.LessonProgresses;
using OriginalLanguage.Services.TaskAnswerChecker;
using OriginalLanguage.Services.TaskAnswerChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.LessonCompletion;
public class LessonCompletionService : ILessonCompletionService
{
    private readonly ITaskAnswerChecker taskAnswerChecker;
    private readonly ILessonProgressesService lessonProgressesService;

    public LessonCompletionService(
        ITaskAnswerChecker taskAnswerChecker,
        ILessonProgressesService lessonProgressesService)
    {
        this.taskAnswerChecker = taskAnswerChecker;
        this.lessonProgressesService = lessonProgressesService;
    }

    public async Task<LessonCompletionResult> TryCompleteLesson(
        int lessonId,
        IEnumerable<TaskAnswer> answers)
    {
        foreach (var answer in answers) {
            var answerCheckResult = await taskAnswerChecker.Check(answer);
            if (!answerCheckResult.IsCorrect)
                return LessonCompletionResult.Failed;
        }

        // All answers are correct
        await lessonProgressesService.TryIncrementLessonProgress(lessonId);

        return new()
        {
            IsSucceeded = true
        };
    }
}
