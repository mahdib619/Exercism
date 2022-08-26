using System;

using static Helpers;

public struct RationalNumber
{
    public int Num { get; set; }
    public int Denom { get; set; }

    public RationalNumber(int numerator, int denominator = 1)
    {
        var gcd = GCD(numerator, denominator);

        Num = FilpSign(numerator, denominator) / gcd;
        Denom = FilpSign(denominator, denominator) / gcd;
    }

    public static RationalNumber operator +(RationalNumber r1, RationalNumber r2) => new(r1.Num * r2.Denom + r2.Num * r1.Denom, r1.Denom * r2.Denom);

    public static RationalNumber operator -(RationalNumber r1, RationalNumber r2) => new(r1.Num * r2.Denom - r2.Num * r1.Denom, r1.Denom * r2.Denom);

    public static RationalNumber operator *(RationalNumber r1, RationalNumber r2) => new(r1.Num * r2.Num, r1.Denom * r2.Denom);

    public static RationalNumber operator /(RationalNumber r1, RationalNumber r2) => r1 * r2.Inverse();

    public RationalNumber Inverse() => new(Denom, Num);

    public RationalNumber Abs() => new(Math.Abs(Num), Denom);

    public RationalNumber Reduce() => this;

    public RationalNumber Exprational(int power) => new((int)Math.Pow(Num, power), (int)Math.Pow(Denom, power));

    public double Expreal(int baseNumber) => baseNumber.Expreal(this);
}

public static class RealNumberExtension
{
    public static double Expreal(this int realNumber, RationalNumber r) => NthRoot(Math.Pow(realNumber, r.Num), r.Denom);
}

public static class Helpers
{
    public static int GCD(int a, int b)
    {
        (a, b) = (Math.Abs(a), Math.Abs(b));

        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }

    public static double NthRoot(double a, int n) => Math.Pow(a, 1.0 / n);

    public static int FilpSign(int num, int sign) => num * Math.Sign(sign == 0 ? 1 : sign);
}