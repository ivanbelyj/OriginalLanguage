using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OriginalLanguage.Common.Lessons;
public static class SentenceUtils
{
    private static readonly char[] separators = {
        ' ', '\t', '`', '!', '\"', '-', ':', ';', '\'', ',', '.', '?'
    };

    private static HashSet<char> separatorsSet = new HashSet<char>(separators);

    public static string Normalize(string s)
    {
        return string.Join(' ', SplitToElements(s));
    }

    public static List<string> SplitToElements(string sentence)
    {
        var res = new List<string>();
        var currentWord = new StringBuilder();
        
        for (int i = 0; i < sentence.Length; i++)
        {
            bool isWordContinuing = !separatorsSet.Contains(sentence[i]);

            if (isWordContinuing)
            {
                currentWord.Append(char.ToLower(sentence[i]));
            }

            if ((!isWordContinuing || i == sentence.Length - 1) 
                && currentWord.Length > 0)
            {
                res.Add(currentWord.ToString());
                currentWord.Clear();
            }
        }
        return res;
    }
}
