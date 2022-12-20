using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SimpleLinkedList<T> : IEnumerable<T>
{
    public SimpleLinkedList(T value) => Value = value;

    public SimpleLinkedList(IEnumerable<T> values)
    {
        var first = true;
        foreach (var value in values)
        {
            if (first)
            {
                Value = value;
                first = false;
            }
            else
                Add(value);
        }
    }

    public T Value { get; }

    public SimpleLinkedList<T> Next { get; private set; }

    public SimpleLinkedList<T> Add(T value)
    {
        var current = this;

        while (current.Next is not null)
        {
            current = current.Next;
        }

        current.Next = new(value);

        return this;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = this;
        while (current is not null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}