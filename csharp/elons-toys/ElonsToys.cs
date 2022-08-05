public class RemoteControlCar
{
    private int _drivenDistance;
    private int _remainBattery = 100;

    public static RemoteControlCar Buy() => new();

    public string DistanceDisplay() => $"Driven {_drivenDistance} meters";
    public string BatteryDisplay() => _remainBattery is 0 ? "Battery empty" : $"Battery at {_remainBattery}%";
    public void Drive()
    {
        if (_remainBattery > 0)
        {
            _drivenDistance += 20;
            _remainBattery--;
        }
    }
}
