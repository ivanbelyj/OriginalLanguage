using OriginalLanguage.Services.TaskGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.Utils;
public class TaskTypeUtils
{
    public static LanguageRole GetAnswerLanguageRole(TaskType taskType)
    {
        return taskType switch
        {
            TaskType.ElementsToText
                or TaskType.FillInElementToText
                or TaskType.ElementsToText
                => LanguageRole.Target,
            TaskType.ElementsToTranslation
                or TaskType.TextToTranslation
                => LanguageRole.Source,
            _ => throw new NotImplementedException("Not supported task type")
        };
    }

    public static bool IsAnswerLanguageHasTargetRole(TaskType taskType)
        => GetAnswerLanguageRole(taskType) == LanguageRole.Target;

    public static TaskType GetRandomTaskTypeByProgressLevel(
        int progressLevel,
        Random? random = null)
    {
        random ??= Random.Shared;

        // Todo: define task type by progress level
        return progressLevel <= 5
            ? TaskType.ElementsToText
            : TaskType.TranslationToText;
    }
}
