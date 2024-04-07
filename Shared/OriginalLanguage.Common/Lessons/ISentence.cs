using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Common.Lessons;
public interface ISentence
{
    string? Text { get; }
    string? Translation { get; }
}
