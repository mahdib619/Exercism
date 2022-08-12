using System.Collections.Generic;

public static class ResistorColor
{
    private static readonly string[] _colors = { "black", "brown", "red", "orange", "yellow", "green", "blue", "violet", "grey", "white" };
    private static readonly Dictionary<string, int> _colorsValues = new();

    static ResistorColor()
    {
        for (var i = 0; i < _colors.Length; i++)
            _colorsValues[_colors[i]] = i;
    }

    public static int ColorCode(string color) => _colorsValues[color];
    public static string[] Colors() => _colors;
}