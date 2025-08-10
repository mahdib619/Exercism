using System;
using System.Collections.Generic;
using System.Linq;

public enum YachtCategory
{
    Ones = 1,
    Twos = 2,
    Threes = 3,
    Fours = 4,
    Fives = 5,
    Sixes = 6,
    FullHouse = 7,
    FourOfAKind = 8,
    LittleStraight = 9,
    BigStraight = 10,
    Choice = 11,
    Yacht = 12,
}

public static class YachtGame
{
    public static int Score(int[] dice, YachtCategory category) => category.GetCalculator().Calculate(dice);

    private static YachtCategoryCalculator GetCalculator(this YachtCategory category) => category switch
    {
        >= YachtCategory.Ones and <= YachtCategory.Sixes => new NsCalculator((int)category),
        YachtCategory.FullHouse => new FullHouseCalculator(),
        YachtCategory.FourOfAKind => new FourOfAKindCalculator(),
        YachtCategory.LittleStraight => new LittleStraightCalculator(),
        YachtCategory.BigStraight => new BigStraightCalculator(),
        YachtCategory.Choice => new ChoiceCalculator(),
        YachtCategory.Yacht => new YachtCalculator(),
        _ => throw new ArgumentException("Invalid Category!", nameof(category))
    };

    private static (int item, int count)[] GetItemsCountDesc(this int[] arr) => arr.GroupBy(it => it)
                                                                                   .OrderByDescending(g => g.Count())
                                                                                   .Select(g => (g.Key, g.Count()))
                                                                                   .ToArray();

    private abstract class YachtCategoryCalculator
    {
        public abstract int Calculate(int[] dice);
    }

    private class NsCalculator : YachtCategoryCalculator
    {
        private readonly int _n;
        public NsCalculator(int n) => _n = n;
        public override int Calculate(int[] dice) => _n * dice.Count(num => num == _n);
    }

    private class FullHouseCalculator : YachtCategoryCalculator
    {
        public override int Calculate(int[] dice) => dice.GetItemsCountDesc() is [{ count: 3 }, { count: 2 }] ? dice.Sum() : 0;
    }

    private class FourOfAKindCalculator : YachtCategoryCalculator
    {
        public override int Calculate(int[] dice)
        {
            var (item, maxCount) = dice.GetItemsCountDesc()[0];
            return maxCount >= 4 ? 4 * item : 0;
        }
    }

    private class LittleStraightCalculator : YachtCategoryCalculator
    {
        private static readonly HashSet<int> _littleStraightRule = new() { 1, 2, 3, 4, 5 };
        public override int Calculate(int[] dice) => !_littleStraightRule.Except(dice).Any() ? 30 : 0;
    }

    private class BigStraightCalculator : YachtCategoryCalculator
    {
        private static readonly HashSet<int> _bigStraightRule = new() { 2, 3, 4, 5, 6 };
        public override int Calculate(int[] dice) => !_bigStraightRule.Except(dice).Any() ? 30 : 0;
    }

    private class ChoiceCalculator : YachtCategoryCalculator
    {
        public override int Calculate(int[] dice) => dice.Sum();
    }

    private class YachtCalculator : YachtCategoryCalculator
    {
        public override int Calculate(int[] dice) => dice.GetItemsCountDesc()[0].count == 5 ? 50 : 0;
    }
}