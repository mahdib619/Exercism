using System;
using System.Linq;

public static class Acronym
{
    public static string Abbreviate(string phrase) => string.Join("", phrase.Replace('-', ' ').Replace("_", "").Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(w => char.ToUpper(w[0])));
}