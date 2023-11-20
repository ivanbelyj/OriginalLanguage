using OriginalLanguage.Context.Entities.User;
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
    public AppUser? Author { get; set; }

    public virtual Lesson? Lesson { get; set; }
}
