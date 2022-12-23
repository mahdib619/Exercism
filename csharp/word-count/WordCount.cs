using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class WordCount
{
    private static readonly Regex Regex = new(@"((?!(('\B)|(\B')|[^\w'])).)+");

    public static IDictionary<string, int> CountWords(string phrase)
            => Regex.Matches(phrase.ToLower()).Select(m => m.Value).GroupBy(w => w).ToDictionary(g => g.First(), g => g.Count());
}