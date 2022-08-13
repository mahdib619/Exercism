using System;
using System.Collections.Generic;

public class CircularBuffer<T>
{
    private readonly Queue<T> _items;
    private readonly int _capacity;

    public CircularBuffer(int capacity)
    {
        _items = new Queue<T>();
        _capacity = capacity;
    }

    public T Read() => _items.Count == 0 ? throw new InvalidOperationException("Buffer is empty!") : _items.Dequeue();

    public void Write(T value)
    {
        if (_items.Count == _capacity)
            throw new InvalidOperationException("Buffer is full!");

        _items.Enqueue(value);
    }

    public void Overwrite(T value)
    {
        if (_items.Count == _capacity)
            _items.Dequeue();

        Write(value);
    }

    public void Clear() => _items.Clear();
}