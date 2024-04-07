using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Entities;
/// <summary>
/// LessonSample is the basis for generating lesson tasks.
/// It represents all correct variants of the sentence and
/// its correct variants of translation
/// </summary>
public class LessonSample : EntityBase
{
    /// <summary>
    /// The minimal progress level for which the lesson sample is available
    /// </summary>
    public int MinimalProgressLevel { get; set; }

    public string? MainText { get; set; }
    public string? MainTranslation { get; set; }
    public string? TextHints { get; set; }
    public string? TranslationHints { get; set; }

    public string? Glosses { get; set; }
    public string? Transcription { get; set; }

    public virtual ICollection<Sentence> SentenceVariants { get; set; }

    public int LessonId { get; set; }
    public virtual Lesson Lesson { get; set; }
}
