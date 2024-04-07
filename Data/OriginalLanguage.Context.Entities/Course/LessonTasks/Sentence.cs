using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Entities;

/// <summary>
/// Additional variant of the lesson sample sentence / translation
/// making possible variable user answers
/// </summary>
public class Sentence : EntityBase
{
    public string? Text { get; set; }
    public string? Translation { get; set; }
    
    public int LessonSampleId { get; set; }
    public virtual LessonSample LessonSample { get; set; }
}
