using System;

public class Deque<T>
{
    private Node _first;
    private Node _last;

    public void Push(T value)
    {
        if (_first is null)
            AddFirstNode(value);
        else
            _last = _last.Next = new(value) { Previous = _last };
    }

    public T Pop()
    {
        if (_first == _last)
            return DeleteLastNode();

        var value = _last.Value;
        _last = _last.Previous;
        _last.Next = null;
        return value;
    }

    public void Unshift(T value)
    {
        if (_first is null)
            AddFirstNode(value);
        else
            _first = _first.Previous = new(value) { Next = _first };
    }

    public T Shift()
    {
        if (_first == _last)
            return DeleteLastNode();

        var value = _first.Value;
        _first = _first.Next;
        _first.Previous = null;
        return value;
    }

    private void AddFirstNode(T value) => _first = _last = new(value);

    private T DeleteLastNode()
    {
        var value = _first.Value;
        _first = _last = null;
        return value;
    }

    private class Node
    {
        public Node(T value) => Value = value;

        public T Value { get; set; }
        public Node Next { get; set; }
        public Node Previous { get; set; }
    }
}