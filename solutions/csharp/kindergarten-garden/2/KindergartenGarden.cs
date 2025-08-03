using System.Collections.Generic;

public class KindergartenGarden
{
    private readonly string[] _rows;

    public KindergartenGarden(string diagram) => _rows = diagram.Split('\n');

    public IEnumerable<Plant> Plants(string student)
    {
        var ci = (student[0] - 65) * 2;
        yield return (Plant)_rows[0][ci];
        yield return (Plant)_rows[0][ci + 1];
        yield return (Plant)_rows[1][ci];
        yield return (Plant)_rows[1][ci + 1];
    }
}

public enum Plant
{
    Violets = 'V',
    Radishes = 'R',
    Clover = 'C',
    Grass = 'G'
}