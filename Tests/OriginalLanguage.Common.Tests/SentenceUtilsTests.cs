using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OriginalLanguage.Common.Lessons;

namespace OriginalLanguage.Common.Tests;
internal class SentenceUtilsTests
{
    [Test]
    public void SplitToElementsTests()
    {
        string s1 = "   one ,; TWO! three";
        string s2 = "   One ,; Two three     !";
        string s3 = "one ,; TWO! three";
        string s4 = "one   two three";

        List<string> expected = new List<string> { "one", "two", "three" };

        Assert.AreEqual(expected, SentenceUtils.SplitToElements(s1));
        Assert.AreEqual(expected, SentenceUtils.SplitToElements(s2));
        Assert.AreEqual(expected, SentenceUtils.SplitToElements(s3));
        Assert.AreEqual(expected, SentenceUtils.SplitToElements(s4));
    }
}
