using System;
using System.Collections.Generic;

public class CircularBuffer<T>
{
    private Queue<T> _items;
    private int _count;
    private readonly int _capacity;

    public CircularBuffer(int capacity)
    {
        _items = new Queue<T>();
        _capacity = capacity;
    }

    public T Read()
    {
        if (_count == 0)
            throw new InvalidOperationException("Buffer is empty!");

        _count--;

        return _items.Dequeue();
    }

    public void Write(T value)
    {
        if (_count == _capacity)
            throw new InvalidOperationException("Buffer is full!");

        _items.Enqueue(value);
        _count++;
    }

    public void Overwrite(T value)
    {
        if (_count == _capacity)
            Read();

        Write(value);
    }

    public void Clear()
    {
        _items.Clear();
        _count = 0;
    }
}