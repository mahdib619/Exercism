using System.Collections.Generic;
using System.Linq;

public static class PrimeFactors
{
    public static long[] Factors(long number)
    {
        var factors = new LinkedList<long>();

        var currentFactor = 2;
        while (number > 1)
        {
            if (number % currentFactor == 0)
            {
                factors.AddLast(currentFactor);
                number /= currentFactor;
            }
            else
                currentFactor++;
        }

        return factors.ToArray();
    }
}