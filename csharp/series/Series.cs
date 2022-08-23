using System;
using System.Collections.Generic;

public static class Series
{
    public static string[] Slices(string numbers, int sliceLength)
    {
        if (numbers.Length == 0)
            throw new ArgumentException("Is Empty!", nameof(numbers));

        if (sliceLength <= 0 || sliceLength > numbers.Length)
            throw new ArgumentException("Invalid Length!", nameof(sliceLength));

        var slices = new List<string>();
        int s = 0, e = sliceLength;

        while (e < numbers.Length + 1)
        {
            slices.Add(numbers[s..e]);
            s++;
            e++;
        }

        return slices.ToArray();
    }
}