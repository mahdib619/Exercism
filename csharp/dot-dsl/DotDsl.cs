using System;
using System.Collections;
using System.Collections.Generic;

public class Graph : IEnumerable
{
    private readonly List<object> _rawItems = new();

    private readonly List<Node> _nodes = new();
    public IReadOnlyCollection<Node> Nodes => _nodes;

    private readonly List<Edge> _edges = new();
    public IReadOnlyCollection<Edge> Edges => _edges;

    private readonly List<Attr> _attrs = new();
    public IReadOnlyCollection<Attr> Attrs => _attrs;

    public void Add(object rawItem)
    {
        switch (rawItem)
        {
            case Node node:
                _nodes.Add(node);
                break;
            case Edge edge:
                _edges.Add(edge);
                break;
            default:
                throw new ArgumentException("Invalid item type!", nameof(rawItem));
        }
        _rawItems.Add(rawItem);
    }

    public void Add(string key, string value)
    {
        var attr = new Attr(key, value);
        _attrs.Add(attr);
        _rawItems.Add(attr);
    }

    public IEnumerator GetEnumerator() => _rawItems.GetEnumerator();
}

public class Node : AttributedBase
{
    public Node(string value) => Value = value;

    public string Value { get; }

    protected bool Equals(Node other) => base.Equals(other) && Value == other.Value;
    public override bool Equals(object obj) => obj is Node node && Equals(node);
    public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Value);
}

public class Edge : AttributedBase
{
    public Edge(string from, string to) => (From, To) = (from, to);

    public string From { get; }
    public string To { get; }

    protected bool Equals(Edge other) => base.Equals(other) && From == other.From && To == other.To;
    public override bool Equals(object obj) => obj is Edge edge && Equals(edge);
    public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), From, To);
}

public class Attr
{
    public Attr(string key, string value) => (Key, Value) = (key, value);

    public string Key { get; }
    public string Value { get; }

    protected bool Equals(Attr other) => Key == other.Key && Value == other.Value;
    public override bool Equals(object obj) => obj is Attr attr && Equals(attr);
    public override int GetHashCode() => HashCode.Combine(Key, Value);
}

public abstract class AttributedBase : IEnumerable<Attr>
{
    private HashCode _hashCode;
    protected readonly HashSet<Attr> Attrs = new();

    public void Add(string key, string value)
    {
        var attr = new Attr(key, value);

        if (Attrs.Add(attr))
            _hashCode.Add(attr);
    }

    protected bool Equals(AttributedBase other) => Attrs.Count == other.Attrs.Count && Attrs.IsSupersetOf(other.Attrs);
    public override bool Equals(object obj) => obj is AttributedBase attr && Equals(attr);
    public override int GetHashCode() => _hashCode.ToHashCode();

    public IEnumerator<Attr> GetEnumerator() => Attrs.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}