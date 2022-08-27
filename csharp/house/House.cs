using System.Linq;
using System.Text;

public static class House
{
    private static readonly (string character, string action)[] _lyricParts = {
        ("horse and the hound and the horn","belonged to"),
        ("farmer sowing his corn","kept"),
        ("rooster that crowed in the morn","woke"),
        ("priest all shaven and shorn","married"),
        ("man all tattered and torn","kissed"),
        ("maiden all forlorn","milked"),
        ("cow with the crumpled horn","tossed"),
        ("dog","worried"),
        ("cat","killed"),
        ("rat","ate"),
        ("malt","lay in"),
        ("house","Jack built")
    };

    public static string Recite(int verseNumber)
    {
        var output = new StringBuilder("This is");

        foreach (var (ch, act) in _lyricParts.TakeLast(verseNumber))
            output.Append($" the {ch} that {act}");

        output.Append('.');
        return output.ToString();
    }

    public static string Recite(int startVerse, int endVerse) => string.Join('\n', Enumerable.Range(startVerse, endVerse - startVerse + 1).Select(i => Recite(i)));
}