using System.Collections;
using System.Collections.Generic;

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

    private static IEnumerable<int> TraverseTree(BinarySearchTree tree)
    {
        if (tree is null)
            yield break;
        
        foreach (var value in TraverseTree(tree.Left))
            yield return value;

        yield return tree.Value;

        foreach (var value in TraverseTree(tree.Right))
            yield return value;
    }

    public IEnumerator<int> GetEnumerator() => TraverseTree(this).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}