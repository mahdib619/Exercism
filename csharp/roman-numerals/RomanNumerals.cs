using System.Collections.Generic;

public static class RomanNumeralExtension
{
    private static readonly List<(int, string)> _digits = new()
    {
        (1000,"M"),
        (900,"CM"),
        (500,"D"),
        (400,"CD"),
        (100,"C"),
        (90,"XC"),
        (50,"L"),
        (40,"XL"),
        (10,"X"),
        (9,"IX"),
        (5,"V"),
        (4,"IV"),
        (1,"I")
    };

    public static string ToRoman(this int value)
    {
        var output = string.Empty;

        foreach (var (v, d) in _digits)
        {
            while (value >= v)
            {
                value -= v;
                output += d;
            }
        }

        return output;
    }
}