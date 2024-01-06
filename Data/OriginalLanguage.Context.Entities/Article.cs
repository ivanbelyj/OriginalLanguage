using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Entities;
public class Article : PageBase
{
    public string? ShortDescription { get; set; }

    public Guid? AuthorId { get; set; }
    public virtual AppUser? Author { get; set; }

    public int? LanguageId { get; set; }
    public virtual Language? Language { get; set; }

    public virtual Lesson? Lesson { get; set; }
}
