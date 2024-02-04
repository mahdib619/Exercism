using System;
using System.Collections.Generic;
using System.Linq;

public static class Transpose
{
    public static string String(string input)
    {
        var lines = input.Split('\n');
        Array.Reverse(lines);
        return string.Join('\n', Enumerable.Range(0, lines.Max(l => l.Length)).Select(i => string.Join(string.Empty, lines.Aggregate((CurrentMaxLength: 0, PaddedLines: new List<string>()), (agg, line) => { agg.PaddedLines.Add(line.PadRight(agg.CurrentMaxLength, ' ')); return (Math.Max(agg.CurrentMaxLength, line.Length), agg.PaddedLines); }, agg => agg.PaddedLines).AsEnumerable().Reverse().Where(el => el.Length > i).Select(el => el[i]))));
    }
}