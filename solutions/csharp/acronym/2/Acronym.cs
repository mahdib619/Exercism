using System;
using System.Linq;
using System.Text.RegularExpressions;

public static class Acronym
{
    public static string Abbreviate(string phrase) => string.Join("", new Regex("[- _]").Replace(phrase, " ").Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(w => char.ToUpper(w[0])));
}