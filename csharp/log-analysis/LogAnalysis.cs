public static class LogAnalysis
{
    public static int IndexAfter(this string input, string value)
    {
        var indexOfFirst = input.IndexOf(value);
        return indexOfFirst == -1 ? -1 : indexOfFirst + value.Length;
    }
    public static string SubstringAfter(this string input, string target) => input[input.IndexAfter(target)..];
    public static string SubstringBetween(this string input, string start, string end) => input[input.IndexAfter(start)..input.IndexOf(end)];
    public static string Message(this string input) => input.SubstringAfter(": ");
    public static string LogLevel(this string input) => input.SubstringBetween("[", "]");
}