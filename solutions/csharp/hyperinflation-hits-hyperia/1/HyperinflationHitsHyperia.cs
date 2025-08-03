using System;
using System.Globalization;

public static class CentralBank
{
    private const string TooBig = "*** Too Big ***";
    private const string MuchTooBig = "*** Much Too Big ***";

    public static string DisplayDenomination(long @base, long multiplier)
    {
        try
        {
            checked
            {
                return (@base * multiplier).ToString();
            }
        }
        catch (OverflowException)
        {
            return TooBig;
        }
    }

    public static string DisplayGDP(float @base, float multiplier)
    {
        var gdp = @base * multiplier;
        return float.IsInfinity(gdp) ? TooBig : gdp.ToString(CultureInfo.InvariantCulture);
    }

    public static string DisplayChiefEconomistSalary(decimal salaryBase, decimal multiplier)
    {
        try
        {
            return (salaryBase * multiplier).ToString(CultureInfo.InvariantCulture);
        }
        catch (OverflowException)
        {
            return MuchTooBig;
        }
    }
}
