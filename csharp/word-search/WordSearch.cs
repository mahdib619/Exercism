using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class WordSearch
{
    private readonly Cell[][] _horizontalGrid;
    private readonly Cell[][] _verticalGrid;
    private readonly Cell[][] _diagonalGridTopRight;
    private readonly Cell[][] _diagonalGridTopLeft;

    public WordSearch(string grid)
    {
        _horizontalGrid = ToCellGrid(grid);
        _verticalGrid = Rotate(_horizontalGrid);
        _diagonalGridTopRight = ToDiagonal(_horizontalGrid, false).ToArray();
        _diagonalGridTopLeft = ToDiagonal(_horizontalGrid, true).ToArray();
    }

    public Dictionary<string, ((int, int), (int, int))?> Search(string[] wordsToSearchFor)
    {
        var output = wordsToSearchFor.ToDictionary(w => w, _ => default(((int, int), (int, int))?));

        foreach (var word in wordsToSearchFor)
        {
            var rWord = Reverse(word);

            output[word] = Search(_horizontalGrid, word, rWord);
            output[word] ??= Search(_verticalGrid, word, rWord);
            output[word] ??= Search(_diagonalGridTopRight, word, rWord);
            output[word] ??= Search(_diagonalGridTopLeft, word, rWord);
        }

        return output;
    }

    private static ((int, int), (int, int))? Search(Cell[][] grid, string word, string rWord)
    {
        if (word.Length > grid[0].Length)
            return null;

        foreach (var row in grid)
        {
            if (FindIndex(row, word, out var start, out var end) || FindIndex(row, rWord, out end, out start))
                return ((start.Column, start.Row), (end.Column, end.Row));
        }

        return null;
    }

    private static string Reverse(string input)
    {
        var chars = input.ToCharArray();
        Array.Reverse(chars);
        return new(chars);
    }

    private static Cell[][] ToCellGrid(string gridStr) => gridStr.Split('\n').Select((row, r) => row.Select((ch, c) => new Cell(r, c, ch)).ToArray()).ToArray();

    private Cell[][] Rotate(Cell[][] grid) => Enumerable.Range(0, grid[0].Length).Select(i => _horizontalGrid.Select(row => row[i]).ToArray()).ToArray();

    private IEnumerable<Cell[]> ToDiagonal(Cell[][] grid, bool fromTopLeft)
    {
        int nRow = grid.Length, nCol = grid[0].Length;

        for (var i = 0; i < nCol; i++)
        {
            var length = Math.Min(nRow, nCol - i);
            var diag = new List<Cell>(length);

            for (var j = 0; j < length; j++)
            {
                var index = fromTopLeft ? new(i + j) : ^(i + j + 1);
                diag.Add(grid[j][index]);
            }

            yield return diag.ToArray();
        }

        for (var i = 1; i < nRow; i++)
        {
            var length = Math.Min(nCol, nRow - i);
            var diag = new List<Cell>(length);

            for (var j = 0; j < length; j++)
            {
                var index = fromTopLeft ? new(j) : ^(j + 1);
                diag.Add(grid[i + j][index]);
            }

            yield return diag.ToArray();
        }
    }

    private static bool FindIndex(Cell[] row, string value, out Cell start, out Cell end)
    {
        int? startIndex = null;
        var srcIndex = 0;

        for (var i = 0; i < row.Length; i++)
        {
            if (row[i].Value == value[srcIndex])
            {
                startIndex ??= i;

                if (srcIndex == value.Length - 1)
                {
                    start = row[startIndex.Value];
                    end = row[i];

                    return true;
                }

                srcIndex++;
            }
            else if (row[i].Value == value[0])
            {
                startIndex = i;
                srcIndex = 1;
            }
            else
            {
                startIndex = null;
                srcIndex = 0;
            }
        }

        (start, end) = (default, default);
        return false;
    }

    private record struct Cell
    {
        public Cell(int ri, int ci, char value)
        {
            Column = ci + 1;
            Row = ri + 1;
            Value = value;
        }

        public int Column { get; }
        public int Row { get; }
        public char Value { get; }
    }
}