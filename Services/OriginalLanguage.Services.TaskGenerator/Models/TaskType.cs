using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.Models;

/// <summary>
/// Text - sentence on the language being studied
/// Translation - sentence on the users language
/// </summary>
public enum TaskType
{
    ElementsToTranslation,
    ElementsToText,
    FillInElementToText,
    TextToTranslation,
    TranslationToText,
}
