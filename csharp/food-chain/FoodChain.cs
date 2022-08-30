using System.Linq;
using System.Text;

public static class FoodChain
{
    private const string FIRST_LINE = "I know an old lady who swallowed a {0}.\n{1}";
    private const string LINE_TEMPLATE = "She swallowed the {0} to catch the {1}.\n";
    private const string LAST_LINE = "I don't know why she swallowed the fly. Perhaps she'll die.";

    private static readonly (string creature, string desc)[] _creatures =
    {
        ("cow","I don't know how she swallowed a cow!"),
        ("goat","Just opened her throat and swallowed a goat!"),
        ("dog","What a hog, to swallow a dog!"),
        ("cat","Imagine that, to swallow a cat!"),
        ("bird","How absurd to swallow a bird!"),
        ("spider","It wriggled and jiggled and tickled inside her."),
        ("fly", null)
    };

    public static string Recite(int verseNumber)
    {
        if (verseNumber is 8)
            return "I know an old lady who swallowed a horse.\nShe's dead, of course!";

        var creatures = _creatures[^verseNumber..];

        var (fcr, desc) = creatures[0];
        var output = new StringBuilder(string.Format(FIRST_LINE, fcr, desc is null ? "" : desc + "\n"));

        for (var i = 0; i < creatures.Length - 1; i++)
            output.AppendFormat(LINE_TEMPLATE, creatures[i].creature, GetNextCreature(i));

        return output.Append(LAST_LINE).ToString();

        string GetNextCreature(int i)
        {
            var creature = creatures[i + 1].creature;
            return creature is "spider" ? $"{creature} that wriggled and jiggled and tickled inside her" : creature;
        }
    }

    public static string Recite(int startVerse, int endVerse) => string.Join("\n\n", Enumerable.Range(startVerse, endVerse - startVerse + 1).Select(i => Recite(i)));
}