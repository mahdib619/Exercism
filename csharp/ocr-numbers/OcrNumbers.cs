using System;
using System.Collections.Generic;
using System.Linq;
public static class OcrNumbers
{
    public static readonly OcrNumber UnreadableInput = new("?", null);
    public static readonly OcrNumber[] Numbers =
    {
        new("0", new[] {" _ ","| |","|_|","   "}),
        new("1", new[] {"   ","  |","  |","   "}),
        new("2", new[] {" _ "," _|","|_ ","   "}),
        new("3", new[] {" _ "," _|"," _|","   "}),
        new("4", new[] {"   ","|_|","  |","   "}),
        new("5", new[] {" _ ","|_ "," _|","   "}),
        new("6", new[] {" _ ","|_ ","|_|","   "}),
        new("7", new[] {" _ ","  |","  |","   "}),
        new("8", new[] {" _ ","|_|","|_|","   "}),
        new("9", new[] {" _ ","|_|"," _|","   "})
    };

    public static string Convert(string input)
    {
        var inputArr = input.Split('\n');

        if (inputArr.Length % 4 != 0 || inputArr.Any(s => s.Length % 3 != 0))
            throw new ArgumentException("Inalid input!", nameof(input));

        var selectorGroups = new LinkedList<NumberSelectorsGroup>();
        for (var i = 0; i < inputArr.Length / 4; i++)
        {
            var selectorsGroup = selectorGroups.AddLast(new NumberSelectorsGroup(inputArr[0])).Value;
            for (var j = 0; j < 4; j++)
                selectorsGroup.Filter(inputArr[i * 4 + j], j);
        }

        return string.Join(',', selectorGroups.Select(sg => sg.GetSelectedNumber()));
    }
}

public class OcrNumber
{
    public OcrNumber(string number, string[] ocrText)
    {
        Number = number;
        OcrText = ocrText;
    }

    public string Number { get; }
    public string[] OcrText { get; }
    public bool ValidateRow(string row, int step) => OcrText[step] == row;
}

public class NumberSelector
{
    private IEnumerable<OcrNumber> _candidates = OcrNumbers.Numbers;
    public void Filter(string row, int step) => _candidates = _candidates.Where(n => n.ValidateRow(row, step));
    public string GetSelectedNumber() => _candidates.SingleOrDefault(OcrNumbers.UnreadableInput).Number;
}

public class NumberSelectorsGroup
{
    private readonly List<NumberSelector> _selectors;
    public NumberSelectorsGroup(int count)
    {
        _selectors = Enumerable.Range(0, count).Select(_ => new NumberSelector()).ToList();
    }

    public NumberSelectorsGroup(string firstRow) : this(firstRow.Length / 3)
    {
    }

    public void Filter(string row, int step)
    {
        for (var i = 0; i < _selectors.Count; i++)
        {
            var rowI = i * 3;
            _selectors[i].Filter(row[rowI..(rowI + 3)], step);
        }
    }

    public string GetSelectedNumber() => string.Join("", _selectors.Select(s => s.GetSelectedNumber()));
}
