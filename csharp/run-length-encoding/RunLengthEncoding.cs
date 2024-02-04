using System.Linq;
using System.Text.RegularExpressions;

public static class RunLengthEncoding
{
    private static readonly Regex _encodeRegex = new(@"(.)\1*");
    private static readonly Regex _decodeRegex = new(@"(\d*.)");

    public static string Encode(string input) => string.Join(string.Empty, _encodeRegex.Matches(input)
                                                                                      .Select(m => $"{(m.Length > 1 ? m.Length.ToString() : string.Empty)}{m.Value[0]}"));

    public static string Decode(string input) => string.Join(string.Empty, _decodeRegex.Matches(input)
                                                                                       .Select(m => m.Length < 2 ? m.Value : new(m.Value[^1], int.Parse(m.Value[..^1]))));
}
