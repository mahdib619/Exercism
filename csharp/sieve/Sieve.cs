using System;
using System.Collections.Generic;
using System.Linq;

public static class Sieve
{
    public static int[] Primes(int limit)
    {
        if (limit < 0)
            throw new ArgumentOutOfRangeException(nameof(limit));

        var composites = new HashSet<int>();
        var primes = new LinkedList<int>();

        for (var i = 2; i <= limit; i++)
        {
            if (composites.Contains(i))
                continue;

            primes.AddLast(i);

            for (var j = i * 2; j <= limit; j += i)
                composites.Add(j);
        }

        return primes.ToArray();
    }
}