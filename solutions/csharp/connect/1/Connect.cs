using System;
using System.Linq;

public class Connect
{
    private readonly string[] _input;

    public Connect(string[] input) => _input = input.Select(x => x.Replace(" ", "")).ToArray();

    public ConnectWinner Result()
    {
        if (_input.Length == 1 && _input[0].Length == 1)
            return (ConnectWinner)_input[0][0];

        return ConnectWinner.None;
    }
}

public enum ConnectWinner
{
    White = 79,
    Black = 88,
    None = 0
}