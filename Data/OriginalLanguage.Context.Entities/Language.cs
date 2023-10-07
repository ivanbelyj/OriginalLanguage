using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Entities;
public class Language : EntityBase
{
    public Language(string name, string nativeName)
    {
        Name = name;
        NativeName = nativeName;
    }
    public int AuthorId { get; set; }
    // Todo: navigation property

    public string Name { get; set; }
    public string NativeName { get; set; }

    public bool IsConlang { get; set; }
    public DateTime DateTimeAdded { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Course> Courses { get; set; }
}
