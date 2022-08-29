using System.Collections.Generic;
using System.Linq;

public static class ParallelLetterFrequency
{
    public static Dictionary<char, int> Calculate(IEnumerable<string> texts) =>
        texts.SelectMany(t => t).AsParallel().Where(char.IsLetter).Select(char.ToLower).Aggregate(new Dictionary<char, int>(), (dic, ch) =>
        {
            dic[ch] = dic.GetValueOrDefault(ch) + 1;
            return dic;
        });
}