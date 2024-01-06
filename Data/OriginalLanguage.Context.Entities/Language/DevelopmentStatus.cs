using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Context.Entities;
public enum DevelopmentStatus
{
    NotSpecified = 0,
    New = 1,
    Progressing = 2,
    Functional = 3,
    Complete = 4,
    OnHold = 5,
    Abandoned = 6,
}
