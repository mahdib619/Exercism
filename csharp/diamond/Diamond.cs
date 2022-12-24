using System;
using System.Linq;

public static class Diamond
{
    public static string Make(char target)
    {
        if (target == 'A')
            return "A";

        var length = SpaceCount(target - 'A' + 1) + 2;

        var diamond = Enumerable.Range(0, length).Select(_ => Enumerable.Repeat(' ', length).ToArray()).ToArray();

        var midRow = (int)Math.Floor(length / 2d);
        var offset = 0;

        while (midRow >= offset)
        {
            var row = diamond[midRow + offset];
            row[offset] = row[^(offset + 1)] = target;

            row = diamond[midRow - offset];
            row[offset] = row[^(offset + 1)] = target;

            offset++;
            target = (char)(target - 1);
        }

        return string.Join('\n', diamond.Select(r => new string(r)));
    }

    private static int SpaceCount(int row) => row * 2 - 3;
}