using System;
using System.Collections.Generic;

public class BowlingGame
{
    private const int PerfectGameScore = 300;

    private readonly List<int> _scores = new(21);
    private int _frameScore;
    private bool _inFrame;

    public void Roll(int pins)
    {
        if (pins is < 0 or > 10)
            throw new ArgumentException("Invalid Score!", nameof(pins));

        if (pins == 10 || )
        {
            _frameScore = 0;
            _lastReset = _scores.Count;
        }

        if (_scores.Count % 2 != 0 && _scores[^1] != 10 && pins != 10 && _scores[^1] + pins > 10)
            throw new ArgumentException("Score in a frame most be less than 10!", nameof(pins));

        _scores.Add(pins);
    }

    public int? Score()
    {
        if (_scores.TrueForAll(f => f == 10))
            return PerfectGameScore;

        var totalScore = 0;

        for (var i = 0; i < _scores.Count; i++)
        {
            var score = _scores[i];

            totalScore += score;

            if (i >= _scores.Count - 4)
                continue;

            if (score == 10)
                totalScore += GetInboundOrDefault(_scores, i + 1) + GetInboundOrDefault(_scores, i + 2);
            else if (score + GetInboundOrDefault(_scores, i + 1) == 10)
                totalScore += GetInboundOrDefault(_scores, i + 2);

        }

        return totalScore;
    }

    private static T GetInboundOrDefault<T>(IReadOnlyList<T> arr, int index) => index >= 0 && index < arr.Count ? arr[index] : default;
}