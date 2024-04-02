using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OriginalLanguage.Common.Lessons;
public static class SentenceUtils
{
    //public static string Normalize(string s)
    //{
    //    return Regex.Replace(s.Trim().ToLower(), @"\s+", " ");
    //}

    private static readonly Regex SpaceRegex = new Regex(@"\s+", RegexOptions.Compiled);

    public static string Normalize(string s)
    {
        return SpaceRegex.Replace(s.Trim().ToLower(), " ");
    }

    public static string[] SplitToElements(string sentence)
    {
        return sentence.Split(" ");
    }
}
