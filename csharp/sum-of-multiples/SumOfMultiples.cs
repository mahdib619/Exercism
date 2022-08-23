using System.Collections.Generic;
using System.Linq;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max)
    {
        var multiplesExcludeZero = multiples.Where(m => m != 0);
        return Enumerable.Range(1, max - 1).Where(n => multiplesExcludeZero.Any(m => n % m == 0)).Sum();
    }
}