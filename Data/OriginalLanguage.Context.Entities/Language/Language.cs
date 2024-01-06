using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Entities;
public class Language : EntityBase
{
    public Guid AuthorId { get; set; }
    public virtual AppUser Author { get; set; }

    public string Name { get; set; }
    public string? NativeName { get; set; }

    public string About { get; set; }
    public string? AboutNativeSpeakers { get; set; }
    public string? Links { get; set; }

    public string? FlagUrl { get; set; }

    //public bool IsConlang { get; set; }

    public int? ConlangDataId { get; set; }
    public virtual ConlangData? ConlangData { get; set; }

    public DateTime DateTimeCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateTimeUpdated { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Course> Courses { get; set; }
    public virtual ICollection<Article> Articles { get; set; }
}
