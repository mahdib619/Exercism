using System;

public static class PerfectNumbers
{
    public static Classification Classify(int number) => AliquotSum(number) switch
    {
        1 => Classification.Deficient,
        var s when s < number => Classification.Deficient,
        var s when s > number => Classification.Abundant,
        _ => Classification.Perfect
    };

    private static int AliquotSum(int num)
    {
        if (num <= 0)
            throw new ArgumentOutOfRangeException(nameof(num), "Non positive number!");

        int sum = 1, a = 2;
        var b = num / 2d;

        while (a < b)
        {
            if (b % 1 == 0)
                sum += a + (int)b;

            b = (double)num / ++a;
        }

        return sum;
    }
}

public enum Classification
{
    Perfect,
    Abundant,
    Deficient
}