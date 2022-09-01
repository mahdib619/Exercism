using System;
using System.Collections.Generic;
using System.Linq;

public static class ScaleGenerator
{
    private static readonly string[] _flatPitches = { "A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab" };
    private static readonly string[] _sharpPitches = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };

    private static readonly List<string> _flatKeys = new() { "F", "Bb", "Eb", "Ab", "Db", "Gb", "d", "g", "c", "f", "bb", "eb" };

    public static string[] Chromatic(string tonic)
    {
        var pitches = _flatKeys.Contains(tonic) ? _flatPitches : _sharpPitches;
        var si = Array.FindIndex(pitches, p => p.Equals(tonic, StringComparison.OrdinalIgnoreCase));

        return pitches[si..].Concat(pitches[..si]).ToArray();
    }

    public static string[] Interval(string tonic, string pattern)
    {
        var all = Chromatic(tonic);
        var output = new LinkedList<string>();

        var i = 0;
        foreach (var p in pattern)
        {
            output.AddLast(all[i]);

            i += p switch
            {
                'm' => 1,
                'M' => 2,
                _ => 3
            };

            if (i >= all.Length)
                break;
        }

        return output.ToArray();
    }
}