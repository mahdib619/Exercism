using System;
using System.Collections.Generic;
using System.Linq;

public static class PalindromeProducts
{
    public static (int, IEnumerable<(int, int)>) Largest(int minFactor, int maxFactor)
    {
        var palindroms = GetPalindromes(minFactor, maxFactor);
        return (palindroms.Max, palindroms.Max.GetFactors(minFactor, maxFactor));
    }

    public static (int, IEnumerable<(int, int)>) Smallest(int minFactor, int maxFactor)
    {
        var palindroms = GetPalindromes(minFactor, maxFactor);
        return (palindroms.Min, palindroms.Min.GetFactors(minFactor, maxFactor));
    }

    private static SortedSet<int> GetPalindromes(int min, int max)
    {
        var palindromes = new SortedSet<int>();

        for (int i = min; i <= max; i++)
        {
            for (int j = min; j <= max; j++)
            {
                var p = i * j;
                if (!palindromes.Contains(p) && p.IsPalindrome())
                    palindromes.Add(p);
            }
        }

        return palindromes.Count == 0 ? throw new ArgumentException("Invalid range!") : palindromes;
    }

    private static IEnumerable<(int, int)> GetFactors(this int number, int min, int max)
    {
        var factors = new HashSet<(int, int)>();

        for (int i = min; i <= max; i++)
        {
            for (int j = min; j <= max; j++)
            {
                if (i * j == number)
                    factors.Add((Math.Min(i, j), Math.Max(i, j)));
            }
        }

        return factors;
    }

    private static bool IsPalindrome(this int number) => number.ToString().IsEqualToReverse();

    private static bool IsEqualToReverse(this string str)
    {
        var strArr = str.ToCharArray();
        Array.Reverse(strArr);
        return strArr.SequenceEqual(str);
    }
}
