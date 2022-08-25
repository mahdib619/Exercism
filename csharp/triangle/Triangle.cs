using System;
using System.Linq;

public static class Triangle
{
    public static bool IsScalene(double side1, double side2, double side3)
    {
        var sides = new[] { side1, side2, side3 };
        return IsTriangle(sides) && sides.All(s => sides.Where(ss => ss == s).Count() == 1);
    }

    public static bool IsIsosceles(double side1, double side2, double side3)
    {
        var sides = new[] { side1, side2, side3 };
        return IsTriangle(sides) && sides.Where(s => sides.Where(ss => ss == s).Count() > 1).Count() > 1;
    }

    public static bool IsEquilateral(double side1, double side2, double side3)
    {
        var sides = new[] { side1, side2, side3 };
        return IsTriangle(sides) && sides.All(s => s == side1);
    }

    private static bool IsTriangle(double[] sides) => !sides.Any(s => s == 0 || sides.Sum() - s < s);
}