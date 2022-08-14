public static class SpiralMatrix
{
    private static readonly (int x, int y)[] _directions = { (0, 1), (1, 0), (0, -1), (-1, 0) };

    public static int[,] GetMatrix(int size)
    {
        var spiralMatrix = new int[size, size];

        var currentDirectionI = 0;
        int x = 0, y = -1;

        for (var i = 1; i <= size * size; i++)
        {
            var (xd, yd) = _directions[currentDirectionI];
            var (tx, ty) = (x + xd, y + yd);

            if (!spiralMatrix.IsInBound(tx, ty) || spiralMatrix[tx, ty] != 0)
            {
                currentDirectionI = (currentDirectionI + 1) % 4;
                (xd, yd) = _directions[currentDirectionI];
                (tx, ty) = (x + xd, y + yd);
            }

            (x, y, spiralMatrix[tx, ty]) = (tx, ty, i);
        }

        return spiralMatrix;
    }

    private static bool IsInBound<T>(this T[,] array, int i, int j) => i >= 0 && j >= 0 && i < array.GetLength(0) && j < array.GetLength(1);
}
