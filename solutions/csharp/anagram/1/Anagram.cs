using System;
using System.Collections.Generic;
using System.Linq;

public class Anagram
{
    private static readonly CharIncaseSensitveEqualityComparer _eqComparer = new();
    private static readonly CharIncaseSensitveComparer _comparer = new();

    private readonly string _baseWord;
    private readonly List<char> _sequencedChars;

    public Anagram(string baseWord) => (_baseWord, _sequencedChars) = (baseWord, GetSequencedChars(baseWord).ToList());

    public string[] FindAnagrams(string[] potentialMatches) => potentialMatches.Where(IsAnagram).ToArray();

    private bool IsAnagram(string str) => !str.Equals(_baseWord, StringComparison.OrdinalIgnoreCase) && _sequencedChars.SequenceEqual(GetSequencedChars(str), _eqComparer);

    private static IEnumerable<char> GetSequencedChars(string str) => str.OrderBy(ch => ch, _comparer);

    private class CharIncaseSensitveEqualityComparer : IEqualityComparer<char>
    {
        public bool Equals(char x, char y) => char.ToLower(x).Equals(char.ToLower(y));
        public int GetHashCode(char obj) => char.ToLower(obj).GetHashCode();
    }

    private class CharIncaseSensitveComparer : IComparer<char>
    {
        public int Compare(char x, char y) => char.ToLower(x).CompareTo(char.ToLower(y));
    }
}