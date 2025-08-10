using System.Text;
using System.Text.RegularExpressions;

public static class Markdown
{
    private static readonly Regex _doubleUnderline = new("__(.+)__");
    private static readonly Regex _singleUnderline = new("_(.+)_");
    private static readonly Regex _headings = new(@"^#+ ");

    private static string Wrap(this string text, string tag) => $"<{tag}>{text}</{tag}>";

    private static string ParseStrongTag(this string text) => _doubleUnderline.Replace(text, "$1".Wrap("strong"));

    private static string ParseEmTag(this string text) => _singleUnderline.Replace(text, "$1".Wrap("em"));

    private static string ParseHeadings(this string text)
    {
        var rawHeadingMatch = _headings.Match(text);
        if (!rawHeadingMatch.Success || (rawHeadingMatch.Value.Length - 1 is var headingNum && headingNum > 6))
            return text;

        var headingTag = $"h{headingNum}";

        return _headings.Replace(text, string.Empty).Wrap(headingTag);
    }

    private static string ParseParagraph(this string text) => text.StartsWithTag("h") ? text : text.Wrap("p");

    private static bool StartsWithTag(this string text, string tag) => text.StartsWith($"<{tag}");

    private static string ParseLine(string markdown)
    {
        return markdown.ParseHeadings()
                       .ParseParagraph()
                       .ParseStrongTag()
                       .ParseEmTag();
    }

    private static string ParseListItem(string markdown)
    {
        return markdown.ParseStrongTag()
                       .ParseEmTag()
                       .Wrap("li");
    }

    public static string Parse(string markdown)
    {
        var wasInList = false;
        var output = new StringBuilder();

        foreach (var line in markdown.Split('\n'))
        {
            var isListItemLine = line.StartsWith("* ");

            if (isListItemLine)
            {
                if (!wasInList)
                    output.Append("<ul>");

                output.Append(ParseListItem(line[2..]));
            }
            else
            {
                if (wasInList)
                    output.Append("</ul>");

                output.Append(ParseLine(line));
            }

            wasInList = isListItemLine;
        }

        if (wasInList)
            output.Append("</ul>");

        return output.ToString();
    }
}