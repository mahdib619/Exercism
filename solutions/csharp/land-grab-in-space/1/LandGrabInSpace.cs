using System;
using System.Collections.Generic;
using System.Linq;

public struct Coord
{
    public ushort X { get; }
    public ushort Y { get; }

    public Coord(ushort x, ushort y)
    {
        X = x;
        Y = y;
    }

    public double GetDistance(Coord other) => Math.Sqrt(Math.Pow(other.X - X, 2) + Math.Pow(other.Y - X, 2));
    public double GetDistance() => GetDistance(new(0, 0));
}

public struct Plot
{
    public Coord[] Sides { get; }

    public Plot(Coord side1, Coord side2, Coord side3, Coord side4) => Sides = new[] { side1, side2, side3, side4 };

    public double GetLongesSide()
    {
        var origin = Sides.MaxBy(s => s.GetDistance());
        return Sides.Max(s => s.GetDistance(origin));
    }

    public override bool Equals(object obj)
    {
        if (obj is Plot other)
            return Sides.SequenceEqual(other.Sides);

        return false;
    }
}

public class ClaimsHandler
{
    private readonly List<Plot> _claimedPlots = new();

    public void StakeClaim(Plot plot) => _claimedPlots.Add(plot);

    public bool IsClaimStaked(Plot plot) => _claimedPlots.Contains(plot);

    public bool IsLastClaim(Plot plot) => _claimedPlots.Count > 0 && _claimedPlots.Last().Equals(plot);

    public Plot GetClaimWithLongestSide() => _claimedPlots.MaxBy(p => p.GetLongesSide());
}
