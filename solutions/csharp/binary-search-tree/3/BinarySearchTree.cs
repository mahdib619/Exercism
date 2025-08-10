using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BinarySearchTree : IEnumerable<int>
{
    private readonly int? _value;
    public int Value => _value ?? 0;

    public BinarySearchTree Left { get; private set; }
    public BinarySearchTree Right { get; private set; }

    public BinarySearchTree(int value) => _value = value;
    public BinarySearchTree(IEnumerable<int> values)
    {
        foreach (var val in values)
        {
            if (_value is null)
            {
                _value = val;
                continue;
            }

            Add(val);
        }
    }

    private BinarySearchTree Add(int value)
    {
        if (value <= Value)
            Left = Left?.Add(value) ?? new BinarySearchTree(value);
        else
            Right = Right?.Add(value) ?? new BinarySearchTree(value);

        return this;
    }

    public IEnumerator<int> GetEnumerator()
    {
        foreach (var value in Left ?? Enumerable.Empty<int>())
            yield return value;

        yield return Value;

        foreach (var value in Right ?? Enumerable.Empty<int>())
            yield return value;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}