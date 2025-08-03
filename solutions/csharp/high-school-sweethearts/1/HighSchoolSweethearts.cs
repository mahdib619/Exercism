using System;
using System.Globalization;

public static class HighSchoolSweethearts
{
    private const string _germanExchangeStudents = "{0} and {1} have been dating since {2} - that's {3} hours";
    private const string _banner =
@"
     ******       ******
   **      **   **      **
 **         ** **         **
**            *            **
**                         **
**     L. G.  +  P. R.     **
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
        return string.Format(new CultureInfo("de-DE"), _germanExchangeStudents, studentA, studentB, start.ToString("dd.MM.yyyy"), hours.ToString(".000,00"));
    }
}
