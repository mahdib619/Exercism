using System;
using System.Linq;

public static class ArmstrongNumbers
{
    public static bool IsArmstrongNumber(int number)
    {
        var nstr = number.ToString();
        return nstr.Select(d => Math.Pow(d - 48, nstr.Length)).Sum() == number;
    }
}