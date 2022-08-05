using System.Text.RegularExpressions;

static class LogLine
{
    public static string Message(string logLine) => logLine.Split(":")[1].Trim();
    public static string LogLevel(string logLine)
    {
        var logLevel = logLine.Split(":")[0].Trim();
        var rgx = new Regex(@"[\[\]]");
        return rgx.Replace(logLevel, "").ToLower();
    }
    public static string Reformat(string logLine) => $"{Message(logLine)} ({LogLevel(logLine)})";
}