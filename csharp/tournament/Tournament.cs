using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class Tournament
{
    private const string HEADER = "Team                           | MP |  W |  D |  L |  P\n";

    public static void Tally(Stream inStream, Stream outStream)
    {
        var table = new StringBuilder(HEADER);

        if (inStream.Length > 0)
        {
            var inputBytes = new byte[inStream.Length];
            inStream.Read(inputBytes);
            var input = Encoding.UTF8.GetString(inputBytes);

            foreach (var match in ProcessInput(input))
                table.Append(match.ToString() + '\n');
        }

        table.Remove(table.Length - 1, 1);

        outStream.Write(Encoding.UTF8.GetBytes(table.ToString()));
        outStream.Flush();
    }

    private static IEnumerable<Team> ProcessInput(string input)
    {
        var matches = new Dictionary<string, Team>();

        foreach (var row in input.Split('\n'))
        {
            var game = row.Split(';');

            if (!matches.TryGetValue(game[0], out var teamA))
                matches[game[0]] = teamA = new(game[0]);

            if (!matches.TryGetValue(game[1], out var teamB))
                matches[game[1]] = teamB = new(game[1]);

            switch (game[2])
            {
                case "win":
                    teamA.Won(teamB);
                    break;
                case "loss":
                    teamA.Lost(teamB);
                    break;
                case "draw":
                    teamA.Drew(teamB);
                    break;
            }
        }

        return matches.Values.OrderBy(m => m);
    }

    private class Team : IComparable<Team>
    {
        public Team(string name) => Name = name;

        public string Name { get; }
        public int Win { get; private set; }
        public int Draw { get; private set; }
        public int Loss { get; private set; }

        public int MatchPlayed => Win + Draw + Loss;
        public int Point => Win * 3 + Draw;

        public void Won(Team opponent)
        {
            Win++;
            opponent.Loss++;
        }

        public void Lost(Team opponent)
        {
            Loss++;
            opponent.Win++;
        }

        public void Drew(Team opponent)
        {
            Draw++;
            opponent.Draw++;
        }

        public int CompareTo(Team other)
        {
            var pointCompare = other.Point.CompareTo(Point);
            return pointCompare != 0 ? pointCompare : Name.CompareTo(other.Name);
        }

        public override string ToString() => $"{Name,-31}|  {MatchPlayed} |  {Win} |  {Draw} |  {Loss} |  {Point}";
    }
}
