using System;
using System.Collections.Generic;

public static class NucleotideCount
{
    public static IDictionary<char, int> Count(string sequence)
    {
        var output = new Dictionary<char, int>
        {
            ['A'] = 0,
            ['C'] = 0,
            ['G'] = 0,
            ['T'] = 0
        };

        foreach(var ch in sequence)
        {
            if (!output.ContainsKey(ch))
                throw new ArgumentException("Invalid nucleotide!");

            output[ch]++;
        }

        return output;
    }
}