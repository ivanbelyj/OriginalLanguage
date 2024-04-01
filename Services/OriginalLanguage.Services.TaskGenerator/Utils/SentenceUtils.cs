using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskGenerator.Utils;
internal static class SentenceUtils
{
    public static string[] SplitToElements(string sentence)
    {
        return sentence.Split(" ");
    }
}
