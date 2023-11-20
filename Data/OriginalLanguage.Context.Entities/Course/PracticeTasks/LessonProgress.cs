using OriginalLanguage.Context.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Entities;
public class LessonProgress : EntityBase
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; }

    public int LessonId { get; set; }
    public virtual Lesson Lesson { get; set; }

    /// <summary>
    /// Determines how well the user coped with the practical part of the lesson.
    /// The level of progress determines the types of new practical tasks generated
    /// </summary>
    public int ProgressLevel { get; set; }
}
