using System;

class RemoteControlCar
{
    private readonly int _speed;
    private readonly int _batteryDrain;

    private int _distanceDriven;
    private int _batteryLevel = 100;

    public RemoteControlCar(int speed, int batteryDrain) => (_speed, _batteryDrain) = (speed, batteryDrain);

    public static RemoteControlCar Nitro() => new(50, 4);

    public bool BatteryDrained() => _batteryLevel < _batteryDrain;
    public int DistanceDriven() => _distanceDriven;
    public void Drive()
    {
        if (!BatteryDrained())
        {
            _distanceDriven += _speed;
            _batteryLevel -= _batteryDrain;
        }
    }
}

class RaceTrack
{
    private readonly int _distance;

    public RaceTrack(int distance) => _distance = distance;

    public bool TryFinishTrack(RemoteControlCar car)
    {
        while (car.DistanceDriven() < _distance)
        {
            if (car.BatteryDrained())
                return false;

            car.Drive();
        }

        return true;
    }
}
