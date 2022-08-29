using System.Collections.Generic;
using System.Linq;

public static class Etl
{
    public static Dictionary<string, int> Transform(Dictionary<int, string[]> old) => old.SelectMany(kv => kv.Value, (kv, v) => (kv.Key, v.ToLower())).ToDictionary(kv => kv.Item2, kv => kv.Key);
}