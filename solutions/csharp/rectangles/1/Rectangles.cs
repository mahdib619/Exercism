using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class Rectangles
{
    private static readonly Regex CornersRegex = new(@"\+");
    private static readonly Regex MiddleColsRegex = new(@"[\|\+]{2}");

    public static int Count(string[] rows)
    {
        var count = 0;
        var rowsCorners = new List<List<int>>();

        for (var i = 0; i < rows.Length; i++)
        {
            var row = rows[i];
            var corners = CornersRegex.Matches(row).Select(m => m.Index).ToList();

            count += rowsCorners.Select((t, j) => t.Intersect(corners).GetCombos().Count(c => AreColsValid(c, j, i))).Sum();

            rowsCorners.Add(corners);
        }

        return count;

        bool AreColsValid((int, int) side, int fr, int lr)
        {
            for (var i = fr + 1; i < lr; i++)
            {
                if (!MiddleColsRegex.IsMatch($"{rows[i][side.Item1]}{rows[i][side.Item2]}"))
                    return false;
            }

            return true;
        }
    }

    private static IEnumerable<(T f, T l)> GetCombos<T>(this IEnumerable<T> items)
    {
        var itemsL = items as IReadOnlyCollection<T> ?? items.ToList();
        return itemsL.SelectMany((it, i) => itemsL.Skip(i + 1).Select(ii => (it, ii)));
    }
}