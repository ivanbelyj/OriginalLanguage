using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Entities;
public class AppUser : IdentityUser<Guid>
{
    public string FullName { get; set; }
    public UserStatus Status { get; set; }

    public virtual ICollection<Article>? Articles { get; set; }
    public virtual ICollection<Language>? Languages { get; set; }
    public virtual ICollection<Course>? Courses { get; set; }
    public virtual ICollection<LessonProgress>? LessonProgresses { get; set; }
}
