using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OriginalLanguage.Common.Lessons;
public static class SentenceUtils
{
    private const char ExplicitSeparator = '/';

    private static readonly char[] punctuationSeparators = {
        ' ', '\t', '`', '!', '\"', '-', ':', ';', '\'', ',', '.', '?'
    };

    /// <summary>
    /// Symbols used as separators but not as elements of glossing notation
    /// </summary>
    private static readonly char[] glossesSeparators = {
        ' ', '\t'
    };

    private static HashSet<char> separatorsSet
        = new HashSet<char>(punctuationSeparators);

    public static string Normalize(string s)
    {
        return string.Join(' ', SplitToElements(s, true));
    }

    public static List<string> SplitToElements(
        string sentence,
        bool toLowerCase = true,
        char[]? separators = null)
    {
        var separatorsSet = separators == null
            ? SentenceUtils.separatorsSet : new HashSet<char>(separators);

        var res = new List<string>();
        var currentWord = new StringBuilder();
        
        for (int i = 0; i < sentence.Length; i++)
        {
            bool isWordContinuing = !separatorsSet.Contains(sentence[i]);

            if (isWordContinuing)
            {
                currentWord.Append(toLowerCase
                    ? char.ToLower(sentence[i]) : sentence[i]);
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

    public static string ExplicitlySeparated(string sentence)
    {
        if (sentence.Contains(ExplicitSeparator))
        {
            return sentence;
        } else
        {
            return string.Join(
                ExplicitSeparator,
                SplitToElements(sentence, false, glossesSeparators));
        }
    }
}
