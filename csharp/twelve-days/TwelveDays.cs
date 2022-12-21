using System;
using System.Linq;
using System.Text;

public static class TwelveDays
{
    private const string Starter = "On the {0} day of Christmas my true love gave to me: ";

    private static readonly (string day, string count, string gift)[] _verseParts =
    {
        ("first", "a", "Partridge in a Pear Tree"),
        ("second", "two", "Turtle Doves"),
        ("third", "three", "French Hens"),
        ("fourth", "four", "Calling Birds"),
        ("fifth", "five", "Gold Rings"),
        ("sixth", "six", "Geese-a-Laying"),
        ("seventh", "seven", "Swans-a-Swimming"),
        ("eighth", "eight", "Maids-a-Milking"),
        ("ninth", "nine", "Ladies Dancing"),
        ("tenth", "ten", "Lords-a-Leaping"),
        ("eleventh", "eleven", "Pipers Piping"),
        ("twelfth", "twelve", "Drummers Drumming")
    };

    public static string Recite(int verseNumber)
    {
        var lyric = new StringBuilder(string.Format(Starter, _verseParts[verseNumber - 1].day));

        for (var i = verseNumber - 1; i > 0; i--)
        {
            var versePart = _verseParts[i];
            lyric.Append($"{versePart.count} {versePart.gift}, ");

            if (i == 1)
                lyric.Append("and ");
        }

        var lastPart = _verseParts[0];
        lyric.Append($"{lastPart.count} {lastPart.gift}.");

        return lyric.ToString();
    }

    public static string Recite(int startVerse, int endVerse) => string.Join('\n', Enumerable.Range(startVerse, endVerse - startVerse + 1).Select(vn => Recite(vn)));
}