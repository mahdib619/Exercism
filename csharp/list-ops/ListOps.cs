using System;
using System.Collections.Generic;

public static class ListOps
{
    public static int Length<T>(List<T> input) => input.Count;

    public static List<T> Reverse<T>(List<T> input)
    {
        var output = new List<T>(input.Count);

        for (var i = input.Count - 1; i >= 0; i--)
            output.Add(input[i]);

        return output;
    }

    public static List<TOut> Map<TIn, TOut>(List<TIn> input, Func<TIn, TOut> map)
    {
        var output = new List<TOut>(input.Count);

        foreach (var item in input)
            output.Add(map(item));

        return output;
    }

    public static List<T> Filter<T>(List<T> input, Func<T, bool> predicate)
    {
        var output = new List<T>(input.Count);

        foreach (var item in input)
        {
            if (predicate(item))
                output.Add(item);
        }

        return output;
    }

    public static TOut Foldl<TIn, TOut>(List<TIn> input, TOut start, Func<TOut, TIn, TOut> func)
    {
        foreach (var item in input)
            start = func(start, item);

        return start;
    }

    public static TOut Foldr<TIn, TOut>(List<TIn> input, TOut start, Func<TIn, TOut, TOut> func)
    {
        foreach (var item in Reverse(input))
            start = func(item, start);

        return start;
    }

    public static List<T> Concat<T>(List<List<T>> input)
    {
        var output = new List<T>();

        foreach (var listItem in input)
        {
            foreach (var item in listItem)
            {
                output.Add(item);
            }
        }

        return output;
    }

    public static List<T> Append<T>(List<T> left, List<T> right) => Concat(new List<List<T>> { left, right });
}