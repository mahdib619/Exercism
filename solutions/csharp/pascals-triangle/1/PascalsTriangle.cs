using System.Collections.Generic;
using System.Linq;

public static class PascalsTriangle
{
    public static IEnumerable<IEnumerable<int>> Calculate(int rows)
    {
        if (rows != 0)
        {
            var row = new List<int> { 1 };
            yield return row;

            for (var i = 2; i <= rows; i++)
            {
                var first = new Stack<int>(row);
                first.Push(0);

                var second = new Queue<int>(row);
                second.Enqueue(0);

                row = SumRows(first, second).ToList();
                yield return row;
            }
        }
    }

    private static IEnumerable<int> SumRows(IEnumerable<int> first, IEnumerable<int> second)
    {
        foreach (var (f, s) in first.Zip(second))
            yield return f + s;
    }
}