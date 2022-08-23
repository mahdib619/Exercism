using System;

public static class DifferenceOfSquares
{
    public static int CalculateSquareOfSum(int n) => (int)Math.Pow((n * (n + 1)) / 2, 2);

    public static int CalculateSumOfSquares(int n) => (n * (n + 1) * (2 * n + 1)) / 6;

    public static int CalculateDifferenceOfSquares(int n) => CalculateSquareOfSum(n) - CalculateSumOfSquares(n);
}