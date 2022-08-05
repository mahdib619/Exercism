class BirdCount
{
    private const int BUSY_DAY_BIRDS_COUNT = 5;

    private readonly int[] _birdsPerDay;

    public BirdCount(int[] birdsPerDay)
    {
        _birdsPerDay = birdsPerDay;
    }

    public static int[] LastWeek() => new[] { 0, 2, 5, 3, 7, 8, 4 };

    public int Today() => _birdsPerDay[6];
    public void IncrementTodaysCount() => _birdsPerDay[6]++;
    public bool HasDayWithoutBirds()
    {
        foreach (var day in _birdsPerDay)
        {
            if (day == 0)
                return true;
        }

        return false;
    }
    public int CountForFirstDays(int numberOfDays)
    {
        var sum = 0;

        for(var i = 0; i < numberOfDays; i++)
            sum += _birdsPerDay[i];

        return sum;
    }
    public int BusyDays()
    {
        var busyDaysCount = 0;

        foreach(var day in _birdsPerDay)
        {
            if (day >= BUSY_DAY_BIRDS_COUNT)
                busyDaysCount++;
        }

        return busyDaysCount;
    }
}
