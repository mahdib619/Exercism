using System;
using System.Collections.Generic;
using System.Linq;

public static class Change
{
    public static int[] FindFewestCoins(int[] coins, int target)
    {
        var a = new SortedSet<int>(coins);
        var (_, c) = Aa(a.GetViewBetween(0, target), target, new());
        return c.ToArray();
    }

    private static (int remain, List<int> coins) Aa(SortedSet<int> coins, int target, List<int> currentCoins)
    {
        if (target == 0)
            return (0, currentCoins);

        while (coins.Count > 0)
        {
            var nt = target - coins.Max;
            currentCoins.Add(coins.Max);
            var result = Aa(coins.GetViewBetween(1, nt), nt, currentCoins);

            if (result.remain == 0)
                return (0, currentCoins);

            coins = coins.GetViewBetween(0, coins.Max - 1);
            currentCoins.RemoveAt(currentCoins.Count - 1);
        }

        throw new Exception();
    }
}