using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public static class Grep
{
    public static string Match(string pattern, string flags, string[] files) => string.Join('\n', GetLines(pattern, flags, files).Distinct());

    private static IEnumerable<string> GetLines(string pattern, string flags, string[] files)
    {
        var flagsArr = flags.Split(' ');
        var selctor = GetSelector(flagsArr, files.Length > 1);
        var comparer = GetComparer(flagsArr);

        foreach (var filePath in files)
        {
            var lineNumber = 1;
            foreach (var line in File.ReadLines(filePath))
            {
                if (comparer(line, pattern))
                    yield return selctor(line, filePath, lineNumber);

                lineNumber++;
            }
        }
    }

    private static Func<string, string, bool> GetComparer(string[] flags)
    {
        var comparison = StringComparison.Ordinal;
        var comparer = (string line, string pattern, StringComparison cmps) => line.Contains(pattern, cmps);
        var invert = false;

        foreach (var flag in flags)
        {
            switch (flag)
            {
                case "-i":
                    comparison = StringComparison.OrdinalIgnoreCase;
                    break;
                case "-v":
                    invert = true;
                    break;
                case "-x":
                    comparer = (line, pattern, cmps) => line.Equals(pattern, cmps);
                    break;
            }
        }

        return (line, pattern) => comparer(line, pattern, comparison) == !invert;
    }

    private static Func<string, string, int, string> GetSelector(string[] flags, bool multiFile)
    {
        var matchSelector = (string line, string fileName) => line;
        var lineNumberSelector = (int lineNumber) => string.Empty;
        Func<string, string> fileNameSelector = multiFile ? fileName => $"{fileName}:" : _ => string.Empty;

        var nameOnly = false;

        foreach (var flag in flags)
        {
            switch (flag)
            {
                case "-l":
                    nameOnly = true;
                    matchSelector = (_, fileName) => fileName;
                    lineNumberSelector = _ => string.Empty;
                    fileNameSelector = _ => string.Empty;
                    break;
                case "-n" when !nameOnly:
                    lineNumberSelector = lineNumber => $"{lineNumber}:";
                    break;
            }
        }

        return (line, fileName, lineNumber) => $"{fileNameSelector(fileName)}{lineNumberSelector(lineNumber)}{matchSelector(line, fileName)}";
    }
}