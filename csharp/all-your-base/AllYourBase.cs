using System;
using System.Collections.Generic;
using System.Linq;

public static class AllYourBase
{
    public static int[] Rebase(int inputBase, int[] inputDigits, int outputBase)
    {
        if (inputBase <= 1 || outputBase <= 1)
            throw new ArgumentException("Base must be more than 1!");

        if (inputDigits.Any(d => d < 0 || d >= inputBase))
            throw new ArgumentException("one or more digits are invalid!", nameof(inputDigits));

        var base10 = inputDigits.Select((d, p) => (int)(d * Math.Pow(inputBase, inputDigits.Length - p - 1))).Sum();

        var outputDigits = new Stack<int>();
        do
        {
            outputDigits.Push(base10 % outputBase);
            base10 /= outputBase;
        } while (base10 > 0);

        return outputDigits.ToArray();
    }
}