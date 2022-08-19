using System.Collections.Generic;
using System.Linq;

public static class Dominoes
{
    public static bool CanChain(IEnumerable<(int left, int right)> dominos)
    {
        var options = new HashSet<Domino>(dominos.Select(d => new Domino(d.left, d.right)));

        foreach (var option in options)
        {
            var currentOptions = new HashSet<Domino>(options);
            currentOptions.Remove(option);

            if (TraverseChain(option.Left, currentOptions) == option.Right)
                return true;

            if (TraverseChain(option.Right, new(currentOptions)) == option.Left)
                return true;
        }

        return options.Count == 0;
    }

    private static int? TraverseChain(int need, HashSet<Domino> options)
    {
        foreach (var option in options)
        {
            var otherSide = option.GetOtherSideIfMatches(need);
            if (otherSide is int os)
            {
                var currentOptions = new HashSet<Domino>(options);
                currentOptions.Remove(option);

                var lastNeed = TraverseChain(os, currentOptions);
                if (lastNeed is not null)
                    return lastNeed;
            }
        }

        return options.Count == 0 ? need : null;
    }

    private class Domino
    {
        public int Left { get; }
        public int Right { get; }

        public Domino(int left, int right) => (Left, Right) = (left, right);

        public int? GetOtherSideIfMatches(int neededSide) => neededSide switch
        {
            _ when neededSide == Left => Right,
            _ when neededSide == Right => Left,
            _ => null
        };
    }
}