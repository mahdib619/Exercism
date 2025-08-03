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

    public BinarySearchTree Add(int value) => Add(value, this);
    private BinarySearchTree Add(int value, BinarySearchTree parent)
    {
        if (value > parent.Value)
            return parent.Right is null ? parent.Right = new(value) : Add(value, parent.Right);

        return parent.Left is null ? parent.Left = new(value) : Add(value, parent.Left);
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