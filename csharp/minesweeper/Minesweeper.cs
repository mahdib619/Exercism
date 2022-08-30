using System;

public static class Minesweeper
{
    private static readonly (int x, int y)[] _dirs = { (-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1) };

    public static string[] Annotate(string[] input)
    {
        var output = new string[input.Length];

        for (var r = 0; r < input.Length; r++)
        {
            var row = input[r].ToCharArray();
            for (var c = 0; c < row.Length; c++)
            {
                var col = row[c];
                if (col is '*')
                    continue;

                var mineCount = 0;
                foreach (var (x, y) in _dirs)
                {
                    var (cf, rf) = (c + x, r + y);
                    if (cf < 0 || rf < 0 || cf >= row.Length || rf >= input.Length)
                        continue;

                    if (input[rf][cf] is '*')
                        mineCount++;
                }

                row[c] = mineCount > 0 ? (char)('0' + mineCount) : ' ';
            }

            output[r] = string.Join("", row);
        }

        return output;
    }
}
