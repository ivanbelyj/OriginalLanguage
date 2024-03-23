﻿using OriginalLanguage.Services.TaskGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator;
internal class TaskTypeUtils
{
    public static LanguageRole GetTaskSentenceLanguageRole(TaskType taskType)
    {
        return taskType switch
        {
            TaskType.ElementsToTranslation
                or TaskType.ElementsToText
                or TaskType.FillInElementToText
                or TaskType.TextToTranslation
                => LanguageRole.Target,
            TaskType.TranslationToText
                => LanguageRole.Source,
            _ => throw new NotImplementedException("Not supported task type")
        };
    }

    public static TaskType GetRandomTaskTypeByProgressLevel(
        int progressLevel,
        Random? random = null)
    {
        random ??= Random.Shared;

        // Todo: define task type by progress level
        return progressLevel <= 5
            ? TaskType.ElementsToTranslation
            : TaskType.TranslationToText;
    }
}
