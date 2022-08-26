using System.Text;

public static class BeerSong
{
    private const string MAIN_LYRIC = "{0} bottle{2} of beer on the wall, {0} bottle{2} of beer.\nTake {4} down and pass it around, {1} bottle{3} of beer on the wall.";
    private const string NO_BOTTLE_LYRIC = "No more bottles of beer on the wall, no more bottles of beer.\nGo to the store and buy some more, 99 bottles of beer on the wall.";

    public static string Recite(int startBottles, int takeDown)
    {
        var output = new StringBuilder();

        for (var i = startBottles; i > startBottles - takeDown; i--)
        {
            if (i < startBottles)
                output.Append("\n\n");

            if (i == 0)
                output.Append(NO_BOTTLE_LYRIC);
            else
            {
                var args = i == 1 ? new object[] { i, "no more", "", "s", "it" } : new object[] { i, i - 1, "s", i == 2 ? "" : "s", "one" };
                output.AppendFormat(MAIN_LYRIC, args);
            }
        }

        return output.ToString();
    }
}