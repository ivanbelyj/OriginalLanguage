using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Entities;
public class Article : PageBase
{
    public string? ShortDescription { get; set; }

    public int AuthorId { get; set; }
    // Todo: navigation property

    //public bool IsLessonTheory { get; set; }
    public virtual Lesson? Lesson { get; set; }
}
