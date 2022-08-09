using System;

public class WeighingMachine
{
    public WeighingMachine(int precision)
    {
        Precision = precision;
    }

    public int Precision { get; }

    private double _weight;
    public double Weight
    {
        get => _weight;
        set => _weight = value > 0 ? value : throw new ArgumentOutOfRangeException(nameof(Weight));
    }

    public string DisplayWeight => $"{(Weight - TareAdjustment).ToString($"f{Precision}")} kg";
    public double TareAdjustment { get; set; } = 5;
}
