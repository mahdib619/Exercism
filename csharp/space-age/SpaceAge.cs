using System;

public class SpaceAge
{
    private readonly int _seconds;

    public SpaceAge(int seconds) => _seconds = seconds;

    private double SecondToYear(double earthYear) => Math.Round(_seconds / 60d / 60d / 24d / 365.25 / earthYear, 2);

    public double OnEarth() => SecondToYear(1);
    public double OnMercury() => SecondToYear(0.2408467);
    public double OnVenus() => SecondToYear(0.61519726);
    public double OnMars() => SecondToYear(1.8808158);
    public double OnJupiter() => SecondToYear(11.862615);
    public double OnSaturn() => SecondToYear(29.447498);
    public double OnUranus() => SecondToYear(84.016846);
    public double OnNeptune() => SecondToYear(164.79132);
}