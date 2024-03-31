using OriginalLanguage.Context.Entities;
using OriginalLanguage.Services.LessonProgresses;
using OriginalLanguage.Services.LessonProgresses.Models;
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
        Guid userId,
        int lessonId,
        IEnumerable<TaskAnswer> answers)
    {
        foreach (var answer in answers) {
            var answerCheckResult = await taskAnswerChecker.Check(answer);
            if (!answerCheckResult.IsCorrect)
                return LessonCompletionResult.Failed;
        }

        await HandleSuccessfulCompletion(lessonId, userId);

        return new()
        {
            IsSucceeded = true
        };
    }

    private async Task HandleSuccessfulCompletion(int lessonId, Guid userId)
    {
        var existingProgress = await lessonProgressesService
            .TryGetByUserAndLessonIds(userId, lessonId);
        if (existingProgress == null)
        {
            await CreateLessonProgress(lessonId, userId);
        } else
        {
            await lessonProgressesService
                .IncrementLessonProgress(existingProgress.Id);
        }
    }

    private async Task CreateLessonProgress(int lessonId, Guid userId)
    {
        AddLessonProgressModel model = new()
        {
            LessonId = lessonId,
            ProgressLevel = Constants.MinProgressLevel,
            UserId = userId
        };
        await lessonProgressesService.AddLessonProgress(model);
    }
}
