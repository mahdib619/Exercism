using System.Linq;

public class Matrix
{
    private readonly int[][] _matrix;

    public Matrix(string input) => _matrix = input.Split('\n').Select(r => r.Split().Select(int.Parse).ToArray()).ToArray();

    public int[] Row(int row) => _matrix[row - 1];

    public int[] Column(int col) => _matrix.Select(r => r[col - 1]).ToArray();
}