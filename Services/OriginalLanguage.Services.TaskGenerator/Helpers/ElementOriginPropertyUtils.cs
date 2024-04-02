using OriginalLanguage.Services.Sentences.Models;
using OriginalLanguage.Services.TaskGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.Helpers;
public static class ElementOriginPropertyUtils
{
    public static Func<SentenceModel, string?> ToFunc(ElementOriginProperty property)
    {
        return property switch
        {
            ElementOriginProperty.Text => (sentence) => sentence.Text,
            ElementOriginProperty.Translation => (sentence) => sentence.Translation,
            _ => throw new InvalidOperationException(
                "Not supported element origin property") 
        };
    }

    public static ElementOriginProperty ByTaskType(TaskType taskType)
    {
        return taskType switch
        {
            TaskType.ElementsToTranslation => ElementOriginProperty.Translation,
            TaskType.ElementsToText => ElementOriginProperty.Text,
            _ => throw new InvalidOperationException(
                "Given task type does not support elements")
        };
    }
}
