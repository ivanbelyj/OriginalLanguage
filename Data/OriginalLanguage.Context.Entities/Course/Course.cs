using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Entities;
public class Course : EntityBase
{
    public int AuthorId { get; set; }
    public int? LanguageId { get; set; }
    public virtual Language? Language { get; set; }
    public string? Title { get; set; }
    public DateTime DateTimeAdded { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Lesson>? Lessons { get; set; }
}
