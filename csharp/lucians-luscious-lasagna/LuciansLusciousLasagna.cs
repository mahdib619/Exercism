class Lasagna
{
    public int ExpectedMinutesInOven() => 40;
    public int RemainingMinutesInOven(int passed) => ExpectedMinutesInOven() - passed;
    public int PreparationTimeInMinutes(int layers) => 2 * layers;
    public int ElapsedTimeInMinutes(int layers, int preparationTime) => PreparationTimeInMinutes(layers) + preparationTime;
}