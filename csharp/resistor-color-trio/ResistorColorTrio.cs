using System;
using System.Collections.Generic;

public static class ResistorColorTrio
{
    private static readonly Dictionary<string, int> _colorValues = new()
    {
        ["black"] = 0,
        ["brown"] = 1,
        ["red"] = 2,
        ["orange"] = 3,
        ["yellow"] = 4,
        ["green"] = 5,
        ["blue"] = 6,
        ["violet"] = 7,
        ["grey"] = 8,
        ["white"] = 9
    };
    public static string Label(string[] colors)
    {
        var firstDigit = _colorValues[colors[0]];
        var secondDigit = _colorValues[colors[1]].ToString();
        var zeroCount = _colorValues[colors[2]];

        (zeroCount, secondDigit) = secondDigit == "0" ? (zeroCount + 1, "") : (zeroCount, secondDigit);
        (zeroCount, var unit) = zeroCount >= 3 ? (zeroCount - 3, "kiloohms") : (zeroCount, "ohms");

        var value = int.Parse($"{firstDigit}{secondDigit}") * Math.Pow(10, zeroCount);
        return $"{value} {unit}";
    }
}
