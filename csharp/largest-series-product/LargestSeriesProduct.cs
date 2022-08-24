using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class LargestSeriesProduct
{
    public static long GetLargestProduct(string digits, int span)
    {
        if (span < 0 || span > digits.Length)
            throw new ArgumentException("Invalid Length!", nameof(span));

        if (!new Regex(@"^\d*$").IsMatch(digits))
            throw new ArgumentException("Invalid Charachter!", nameof(digits));

        return span == 0 ? 1 : digits.GetSeries(span).Max(s => s.GetDigitsMultiply());
    }
    private static long GetDigitsMultiply(this string digits) => digits.Aggregate(1, (mul, d) => mul * int.Parse(d.ToString()));
    private static IEnumerable<string> GetSeries(this string str, int seriesLength)
    {
        int s = 0, e = seriesLength;

        while (e < str.Length + 1)
        {
            yield return str[s..e];
            s++;
            e++;
        }
    }
}