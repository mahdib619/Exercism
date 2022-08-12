using System;
using System.Globalization;

public static class HighSchoolSweethearts
{
    private const string _germanExchangeStudents = "{0} and {1} have been dating since {2:d} - that's {3:n2} hours";
    private const string _banner =
@"
     ******       ******
   **      **   **      **
 **         ** **         **
**            *            **
**                         **
**     {0} +  {1}    **
 **                       **
   **                   **
     **               **
       **           **
         **       **
           **   **
             ***
              *
";

    public static string DisplaySingleLine(string studentA, string studentB) => $"{studentA,29} ♡ {studentB,-29}";

    public static string DisplayBanner(string studentA, string studentB) => string.Format(_banner, studentA, studentB);

    public static string DisplayGermanExchangeStudents(string studentA, string studentB, DateTime start, float hours)
    {
        return string.Format(new CultureInfo("de-DE"), _germanExchangeStudents, studentA, studentB, start, hours);
    }
}
