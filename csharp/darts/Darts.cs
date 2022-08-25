using System;

public static class Darts
{
    public static int Score(double x, double y) => Math.Sqrt(x * x + y * y) switch
    {
        > 10 => 0,
        > 5 => 1,
        > 1 => 5,
        _ => 10
    };
}
