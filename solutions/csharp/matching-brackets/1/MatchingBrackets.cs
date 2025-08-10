using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class MatchingBrackets
{
    private static readonly Dictionary<char, char> _bracketPairs = new()
    {
        [']'] = '[',
        [')'] = '(',
        ['}'] = '{'
    };

    public static bool IsPaired(string input)
    {
        input = new Regex(@"(?![\[\]{}()]).").Replace(input, "");
        var leftBrackets = new Stack<char>(" ");

        foreach (var ch in input)
        {
            if (_bracketPairs.GetValueOrDefault(ch, '*') == leftBrackets.Peek())
                leftBrackets.Pop();
            else
                leftBrackets.Push(ch);
        }

        return leftBrackets.Count == 1;
    }
}
