using System;
using System.Linq;
using System.Text.RegularExpressions;

public static class Wordy
{
    private const string OperationsGroup = "op";
    private const string DigitsGroup = "di";
    private static readonly string[] Ops = { "plus", "minus", "multiplied by", "divided by" };
    private static readonly Regex Regex = new(@$"^What is (?<{DigitsGroup}>-?\d+)( (?<{OperationsGroup}>({string.Join("||", Ops)})) (?<{DigitsGroup}>-?\d+))*\?$");

    public static int Answer(string question)
    {
        var match = Regex.Match(question);

        if (!match.Success)
            throw new ArgumentException("Invalid Question!", nameof(question));

        var numbers = match.Groups[DigitsGroup].Captures.Select(c => int.Parse(c.Value)).ToList();
        var ops = match.Groups[OperationsGroup].Captures.Select(c => c.Value);

        return numbers.Skip(1).Zip(ops).Aggregate(numbers[0], (total, cur) => DoOperation(total, cur.First, cur.Second));
    }

    private static int DoOperation(int a, int b, string op) => op switch
    {
        "plus" => a + b,
        "minus" => a - b,
        "multiplied by" => a * b,
        _ => a / b
    };
}