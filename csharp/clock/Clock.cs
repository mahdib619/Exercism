public readonly struct Clock
{
    public int Hours { get; }
    public int Minutes { get; }

    public Clock(int hours, int minutes)
    {
        var h = (hours + minutes / 60) % 24;
        var m = minutes % 60;

        if (m < 0)
            h--;

        Hours = h >= 0 ? h : 24 + h;
        Minutes = m >= 0 ? m : 60 + m;
    }

    public Clock Add(int minutesToAdd) => new(Hours, Minutes + minutesToAdd);

    public Clock Subtract(int minutesToSubtract) => new(Hours, Minutes - minutesToSubtract);

    public override string ToString() => $"{Hours:00}:{Minutes:00}";
}
