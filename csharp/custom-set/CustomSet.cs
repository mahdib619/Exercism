using System;
using System.Collections.Generic;
using System.Linq;

public class CustomSet
{
    private readonly HashSet<int> _values;

    public CustomSet(params int[] values) => _values = new(values);

    private CustomSet(IEnumerable<int> values) => _values = new(values);

    public CustomSet Add(int value)
    {
        _values.Add(value);
        return this;
    }

    public bool Empty() => _values.Count == 0;

    public bool Contains(int value) => _values.Contains(value);

    public bool Subset(CustomSet right) => _values.IsSubsetOf(right._values);

    public bool Disjoint(CustomSet right) => !_values.Overlaps(right._values);

    public CustomSet Intersection(CustomSet right) => new(right._values.Intersect(_values));

    public CustomSet Difference(CustomSet right) => new(_values.Except(right._values));

    public CustomSet Union(CustomSet right) => new(_values.Union(right._values));

    public bool Equals(CustomSet right) => _values.SetEquals(right._values);

    public override bool Equals(object obj) => obj is CustomSet right && Equals(right);
}