using FluentValidation;
using OriginalLanguage.Services.TaskGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskAnswerChecker.Models;

#nullable disable

public class TaskAnswer
{
    public LessonTask Task { get; set; }
    public string Answer { get; set; }
}

public class TaskAnswerValidator : AbstractValidator<TaskAnswer>
{
    public TaskAnswerValidator()
    {
        RuleFor(x => x.Task)
            .NotNull();
        RuleFor(x => x.Answer)
            .NotNull();
    }
}
