using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskAnswerChecker.Models;

public class CheckAnswerResult
{
    public bool IsCorrect { get; set; }
    public TaskAnswer? CorrectAnswer { get; set; }
}
