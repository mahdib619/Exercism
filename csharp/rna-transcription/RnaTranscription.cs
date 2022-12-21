using System.Collections.Generic;
using System.Linq;

public static class RnaTranscription
{
    private static readonly Dictionary<char, char> _complements = new()
    {
        ['G'] = 'C',
        ['C'] = 'G',
        ['T'] = 'A',
        ['A'] = 'U'
    };

    public static string ToRna(string nucleotide) => string.Join(string.Empty, nucleotide.Select(ch => _complements[ch]));
}