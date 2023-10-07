using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Entities;
public class Lesson : EntityBase
{
    public int? TheoryArticleId { get; set; }
    public virtual Article? TheoryArticle { get; set; }

    /// <summary>
    /// Number of the lesson in it's course
    /// </summary>
    public int Number { get; set; }

    public int CourseId { get; set; }
    public virtual Course Course { get; set; }

    public virtual ICollection<LessonProgress>? LessonProgresses { get; set; }
    public virtual ICollection<LessonSample>? LessonSamples { get; set; }
}
