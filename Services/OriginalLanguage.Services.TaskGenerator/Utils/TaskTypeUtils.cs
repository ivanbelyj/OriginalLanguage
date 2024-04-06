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
                or TaskType.TranslationToText
                => LanguageRole.Target,
            TaskType.ElementsToTranslation
                or TaskType.TextToTranslation
                => LanguageRole.Source,
            _ => throw new NotImplementedException("Not supported task type")
        };
    }

    public static bool IsAnswerLanguageHasTargetRole(TaskType taskType)
        => GetAnswerLanguageRole(taskType) == LanguageRole.Target;

    // Debug version
    public static TaskType GetRandomTaskTypeByProgressLevel(
        int progressLevel,
        Random? random = null)
    {
        random ??= new Random();

        TaskType[] values = new[] {
            TaskType.ElementsToText, TaskType.ElementsToTranslation
        }; // Enum.GetValues<TaskType>();
        return values.GetRandomElement(random);
    }

    //public static TaskType GetRandomTaskTypeByProgressLevel(
    //    int progressLevel,
    //    Random? random = null)
    //{
    //    random ??= new Random();

    //    return progressLevel switch
    //    {
    //        <= 2 => new[] {
    //            TaskType.FillInElementToText,
    //            TaskType.ElementsToTranslation,
    //        }.GetRandomElement(random),
    //        <= 5 => new[] {
    //            TaskType.FillInElementToText,
    //            TaskType.ElementsToTranslation,
    //        }.GetRandomElement(random),
    //        _ => new[] {
    //            TaskType.TextToTranslation,
    //            TaskType.TranslationToText
    //        }.GetRandomElement(random)
    //    };
    //}
}
