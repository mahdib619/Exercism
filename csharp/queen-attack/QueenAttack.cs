using System;

public class Queen
{
    public Queen(int row, int column)
    {
        if (row is < 0 or > 7)
            throw new ArgumentOutOfRangeException(nameof(row), row, "Row most be between 0 and 7");

        if (column is < 0 or > 7)
            throw new ArgumentOutOfRangeException(nameof(column), column, "Column most be between 0 and 7");

        Row = row;
        Column = column;
    }

    public int Row { get; }
    public int Column { get; }
}

public static class QueenAttack
{
    public static bool CanAttack(Queen white, Queen black) => white.Row == black.Row || 
                                                              white.Column == black.Column || 
                                                              (Math.Abs(white.Column - black.Column) == Math.Abs(white.Row - black.Row));

    public static Queen Create(int row, int column) => new(row, column);
}