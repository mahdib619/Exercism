using System;
using System.Collections.Generic;
using System.Linq;

public class Reactor
{
    public InputCell CreateInputCell(int value) => new(value);

    public ComputeCell CreateComputeCell(IEnumerable<Cell> producers, Func<int[], int> compute) => new(producers, compute);
}

public abstract class Cell
{
    public event EventHandler<int> Changed;

    protected void OnChanged(object sender, int value) => Changed?.Invoke(sender, value);

    public abstract int GetValue();
}

public class InputCell : Cell
{
    private int _value;
    public int Value
    {
        get => _value;
        set
        {
            if (_value == value)
                return;

            _value = value;
            OnChanged(this, _value);
        }
    }

    public InputCell(int value) => Value = value;

    public override int GetValue() => Value;
}

public class ComputeCell : Cell
{
    private readonly Cell[] _producers;
    private readonly Func<int[], int> _compute;

    private readonly Dictionary<Cell, int> _updatePathCounts = new();
    private readonly Dictionary<(Cell, int), int> _updatesReceivedCount = new();

    public ComputeCell(IEnumerable<Cell> producers, Func<int[], int> compute)
    {
        _producers = producers.ToArray();
        _compute = compute;

        foreach (var producer in _producers)
            producer.Changed += OnDependencyUpdate;

        Update();
    }

    public int Value { get; private set; }

    public override int GetValue() => Value;

    private void Update() => Value = _compute(_producers.Select(p => p.GetValue()).ToArray());

    private void OnDependencyUpdate(object sender, int _)
    {
        if (sender is not Cell dependency)
            throw new ArgumentException("Invalid sender type!", nameof(sender));

        if (!ShouldUpdateByChangeOn(dependency))
            return;

        var oldValue = Value;

        Update();

        if (oldValue != Value)
            OnChanged(sender, Value);
    }

    private bool ShouldUpdateByChangeOn(Cell dependency)
    {
        var key = (dependency, dependency.GetValue());
        _updatesReceivedCount[key] = _updatesReceivedCount.GetValueOrDefault(key) + 1;

        if (_updatesReceivedCount[key] < GetUpdatePathsCount(dependency))
            return false;

        _updatesReceivedCount.Remove(key);
        return true;
    }

    private int GetUpdatePathsCount(Cell dependency)
    {
        return _updatePathCounts.TryGetValue(dependency, out var value) ? value : _updatePathCounts[dependency] = CountUpdatePathsOn();

        int CountUpdatePathsOn() => _producers.OfType<ComputeCell>().Count(cc => cc.DependsOn(dependency)) + (_producers.Any(p => ReferenceEquals(p, dependency)) ? 1 : 0);
    }

    private bool DependsOn(Cell dependency) =>
        _producers.Any(p => ReferenceEquals(p, dependency) || (p is ComputeCell computeProducer && computeProducer.DependsOn(dependency)));
}