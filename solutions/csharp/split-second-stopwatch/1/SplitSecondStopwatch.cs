public enum StopwatchState
{
    Ready,
    Running,
    Stopped
}

public class SplitSecondStopwatch(TimeProvider time)
{
    private DateTimeOffset? _startTime;
    private TimeSpan _previousElapsedTime;
    private readonly LinkedList<TimeSpan> _previousLaps = [];

    public StopwatchState State { get; private set; }
    public TimeSpan CurrentLap => _previousElapsedTime + (time.GetLocalNow() - (_startTime ?? time.GetLocalNow()));
    public TimeSpan Total => PreviousLaps.Aggregate(CurrentLap, (total, current) => total + current);
    public IReadOnlyCollection<TimeSpan> PreviousLaps => _previousLaps;

    public void Start()
    {
        if (State == StopwatchState.Running)
        {
            throw new InvalidOperationException("Timer is already running.");
        }

        State = StopwatchState.Running;

        _startTime = time.GetLocalNow();
    }

    public void Stop()
    {
        if (State != StopwatchState.Running)
        {
            throw new InvalidOperationException("Timer is not running.");
        }

        State = StopwatchState.Stopped;
        _previousElapsedTime += time.GetLocalNow() - _startTime!.Value;
        _startTime = null;
    }

    public void Reset()
    {
        if (State != StopwatchState.Stopped)
        {
            throw new InvalidOperationException("Timer is not stopped.");
        }

        State = StopwatchState.Ready;
        NewCurrentLap();
        _previousLaps.Clear();
    }

    public void Lap()
    {
        if (State != StopwatchState.Running)
        {
            throw new InvalidOperationException("Timer is not running.");
        }

        _previousLaps.AddLast(CurrentLap);
        NewCurrentLap();
    }

    private void NewCurrentLap()
    {
        _previousElapsedTime = TimeSpan.Zero;
        _startTime = time.GetLocalNow();
    }
}