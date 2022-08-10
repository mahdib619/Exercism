public class RemoteControlCar
{
    private Speed _currentSpeed;

    public string CurrentSponsor { get; private set; }
    public ITelemetryHandler Telemetry { get; }

    public RemoteControlCar() => Telemetry = new TelemetryHandler(this);

    public string GetSpeed() => _currentSpeed.ToString();
    private void SetSpeed(Speed speed) => _currentSpeed = speed;

    private void SetSponsor(string sponsorName) => CurrentSponsor = sponsorName;

    public interface ITelemetryHandler
    {
        void Calibrate();
        bool SelfTest();
        void ShowSponsor(string sponsorName);
        void SetSpeed(decimal amount, string unitsString);
    }

    private class TelemetryHandler : ITelemetryHandler
    {
        private readonly RemoteControlCar _car;

        public TelemetryHandler(RemoteControlCar car) => _car = car;

        public void Calibrate()
        {
        }

        public bool SelfTest() => true;

        public void ShowSponsor(string sponsorName) => _car.SetSponsor(sponsorName);

        public void SetSpeed(decimal amount, string unitsString)
        {
            var speedUnits = unitsString == "cps" ? SpeedUnits.CentimetersPerSecond : SpeedUnits.MetersPerSecond;
            _car.SetSpeed(new Speed(amount, speedUnits));
        }
    }

    private enum SpeedUnits
    {
        MetersPerSecond,
        CentimetersPerSecond
    }

    private struct Speed
    {
        public decimal Amount { get; }
        public SpeedUnits SpeedUnits { get; }

        public Speed(decimal amount, SpeedUnits speedUnits) => (Amount, SpeedUnits) = (amount, speedUnits);

        public override string ToString() => $"{Amount} {(SpeedUnits == SpeedUnits.CentimetersPerSecond ? "centimeters per second" : "meters per second")}";
    }
}