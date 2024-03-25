using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskAnswerChecker.Models;
public class LessonCompletionResult
{
    public bool IsSucceeded { get; set; }

    public static LessonCompletionResult Failed => new() { IsSucceeded = false };
}
