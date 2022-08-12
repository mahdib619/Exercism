using System.Text.RegularExpressions;

public class LogParser
{
    public bool IsValidLine(string text) => new Regex(@"^\[[A-Z]{3}\].*").IsMatch(text);

    public string[] SplitLogLine(string text) => new Regex(@"<[\^\*=-]*>").Split(text);

    public int CountQuotedPasswords(string lines) => new Regex("\".*password.*\"", RegexOptions.IgnoreCase).Matches(lines).Count;

    public string RemoveEndOfLineText(string line) => new Regex(@"end-of-line\d*").Replace(line, "");

    public string[] ListLinesWithPasswords(string[] lines)
    {
        var regex = new Regex("password(?! ).*", RegexOptions.IgnoreCase);
        var prefixedLines = new string[lines.Length];

        for (var i = 0; i < lines.Length; i++)
        {
            var matches = regex.Matches(lines[i]);
            var prefix = matches.Count > 0 ? matches[0].Value.Split(" ")[0] : "--------";
            prefixedLines[i] = $"{prefix}: {lines[i]}";
        }

        return prefixedLines;
    }
}
