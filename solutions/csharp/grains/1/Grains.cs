using System;

public static class Grains
{
    public static ulong Square(int n) => n is > 0 and <= 64 ? (ulong)Math.Pow(2, n - 1) : throw new ArgumentOutOfRangeException(nameof(n), "Number should be in range 1-64!");

    public static ulong Total(int n = 64) => n is > 0 and <= 64 ? (ulong)Math.Pow(2, n) - 1 : throw new ArgumentOutOfRangeException(nameof(n), "Number should be in range 1-64!");
}