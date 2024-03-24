using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OriginalLanguage.Services.TaskAnswerChecker;
static class SentenceNormalizer
{
    public static string Normalize(string s)
    {
        return Regex.Replace(s.Trim().ToLower(), @"\s+", " ");
    }
}
