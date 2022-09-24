using System;
using System.Collections.Generic;
using System.Linq;

public static class SaddlePoints
{
    public static IEnumerable<(int, int)> Calculate(int[,] matrix)
    {
        var rowsMin = Enumerable.Range(0, matrix.GetLength(0)).SelectMany(matrix.GetMaxRowsDim);
        var colsMin = Enumerable.Range(0, matrix.GetLength(1)).SelectMany(matrix.GetMinColsDim);

        return rowsMin.Intersect(colsMin);
    }

    private static IEnumerable<(int, int)> GetMaxRowsDim(this int[,] matrix, int colI)
    {
        var maxIndexes = new LinkedList<(int, int)>();
        var max = int.MinValue;

        for (var i = 0; i < matrix.GetLength(1); i++)
        {
            var c = matrix[colI, i];

            if (c >= max)
            {
                if (c > max)
                {
                    maxIndexes.Clear();
                    max = c;
                }

                maxIndexes.AddLast((colI + 1, i + 1));
            }
        }

        return maxIndexes;
    }

    private static IEnumerable<(int, int)> GetMinColsDim(this int[,] matrix, int rowI)
    {
        var minIndexes = new LinkedList<(int, int)>();
        var min = int.MaxValue;

        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            var c = matrix[i, rowI];

            if (c <= min)
            {
                if (c < min)
                {
                    minIndexes.Clear();
                    min = c;
                }

                minIndexes.AddLast((i + 1, rowI + 1));
            }
        }

        return minIndexes;
    }
}