using System;
using System.Collections.Generic;
using System.Linq;

public static class Sieve
{
    public static int[] Primes(int limit)
    {
        if (limit < 0)
            throw new ArgumentOutOfRangeException(nameof(limit));

        var nonPrimes = new HashSet<int>();

        for (var i = 2; i <= limit; i++)
        {
            if (nonPrimes.Contains(i))
                continue;

            for (var j = i * 2; j <= limit; j += i)
                nonPrimes.Add(j);
        }

        return Enumerable.Range(2, limit - 1).Where(n => !nonPrimes.Contains(n)).ToArray();
    }
}