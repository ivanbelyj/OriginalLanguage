using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Entities;
/// <summary>
/// LessonSample is the basis for generating practice tasks.
/// It represents all correct variants of the sentence and
/// its correct variants of translation
/// </summary>
public class LessonSample : EntityBase
{
    /// <summary>
    /// The minimal progress level for which the lesson sample is available
    /// </summary>
    public int MinimalProgressLevel { get; set; }

    public int? MainSentenceVariantId { get; set; }
    public virtual Sentence? MainSentenceVariant { get; set; }

    public virtual ICollection<Sentence> SentenceVariants { get; set; }

    public int LessonId { get; set; }
    public virtual Lesson Lesson { get; set; }
}
