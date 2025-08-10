public class RemoteControlCar
{
    private int _batteryPercentage = 100;
    private int _distanceDrivenInMeters;
    private string[] _sponsors;
    private int _latestSerialNum;

    public static RemoteControlCar Buy() => new();

    public void Drive()
    {
        if (_batteryPercentage > 0)
        {
            _batteryPercentage -= 10;
            _distanceDrivenInMeters += 2;
        }
    }

    public void SetSponsors(params string[] sponsors) => _sponsors = sponsors;

    public string DisplaySponsor(int sponsorNum) => _sponsors[sponsorNum];

    public bool GetTelemetryData(ref int serialNum, out int batteryPercentage, out int distanceDrivenInMeters)
    {
        var result = true;

        if (serialNum < _latestSerialNum)
        {
            serialNum = _latestSerialNum;
            batteryPercentage = -1;
            distanceDrivenInMeters = -1;
            result = false;
        }
        else
        {
            _latestSerialNum = serialNum;
            batteryPercentage = _batteryPercentage;
            distanceDrivenInMeters = _distanceDrivenInMeters;
        }

        return result;
    }
}

public class TelemetryClient
{
    private readonly RemoteControlCar _car;

    public TelemetryClient(RemoteControlCar car) => _car = car;

    public string GetBatteryUsagePerMeter(int serialNum)
    {
        if (!_car.GetTelemetryData(ref serialNum, out var batteryPercentage, out var distanceDrivenInMeters) || distanceDrivenInMeters == 0)
            return "no data";

        return $"usage-per-meter={(100 - batteryPercentage) / distanceDrivenInMeters}";
    }
}
