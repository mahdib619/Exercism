using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class Tournament
{
    public static void Tally(Stream inStream, Stream outStream)
    {
        var table = new StringBuilder("Team                           | MP |  W |  D |  L |  P\n");

        if (inStream.Length > 0)
        {
            var inputBytes = new byte[inStream.Length];
            inStream.Read(inputBytes);
            var input = Encoding.UTF8.GetString(inputBytes);

            var states = new Dictionary<string, Team>();

            foreach (var row in input.Split('\n'))
            {
                var game = row.Split(';');

                if (!states.TryGetValue(game[0], out var teamA))
                    states[game[0]] = teamA = new(game[0]);

                if (!states.TryGetValue(game[1], out var teamB))
                    states[game[1]] = teamB = new(game[1]);

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

            foreach (var item in states.Values.OrderBy(v => v))
                table.Append(item.ToString() + '\n');
        }

        table.Remove(table.Length - 1, 1);

        outStream.Write(Encoding.UTF8.GetBytes(table.ToString()));
        outStream.Flush();
    }

    public class Team : IComparable<Team>
    {
        public Team(string name) => Name = name;

        public string Name { get; }
        public int MatchPlayed { get; private set; }
        public int Win { get; private set; }
        public int Loss { get; private set; }
        public int Draw => MatchPlayed - (Win + Loss);
        public int Point => Win * 3 + Draw;

        public void Won(Team opponent)
        {
            MatchPlayed++;
            Win++;
            opponent.MatchPlayed++;
            opponent.Loss++;
        }

        public void Lost(Team opponent)
        {
            MatchPlayed++;
            Loss++;
            opponent.MatchPlayed++;
            opponent.Win++;
        }

        public void Drew(Team opponent)
        {
            MatchPlayed++;
            opponent.MatchPlayed++;
        }

        public int CompareTo(Team other)
        {
            var pointCompare = other.Point.CompareTo(Point);
            return pointCompare != 0 ? pointCompare : Name.CompareTo(other.Name);
        }

        public override string ToString() => $"{Name,-31}|  {MatchPlayed} |  {Win} |  {Draw} |  {Loss} |  {Point}";
    }
}
