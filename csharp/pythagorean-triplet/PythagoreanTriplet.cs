using System;
using System.Collections.Generic;

public static class PythagoreanTriplet
{
    public static IEnumerable<(int a, int b, int c)> TripletsWithSum(int sum)
    {
        for (var a = 1; a < sum; a++)
        {
            for (var b = a + 1; b <= sum; b++)
            {
                var c = sum - (a + b);
                if (b < c && Math.Pow(a, 2) + Math.Pow(b, 2) == Math.Pow(c, 2))
                    yield return (a, b, c);
            }
        }
    }
}