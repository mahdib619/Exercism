using System;

public class Meetup
{
    private readonly int _month;
    private readonly int _year;

    public Meetup(int month, int year) => (_month, _year) = (month, year);

    public DateTime Day(DayOfWeek dayOfWeek, Schedule schedule)
    {
        DateTime date;
        int addedDays;

        switch (schedule)
        {
            case Schedule.Teenth:
                date = new DateTime(_year, _month, 13);
                addedDays = DaysUntilDayOfWeek(date, dayOfWeek, 1);
                break;
            case Schedule.Last:
                date = new DateTime(_year, _month, DateTime.DaysInMonth(_year, _month));
                addedDays = -DaysPassedFromDayOfWeek(date, dayOfWeek);
                break;
            default:
                date = new DateTime(_year, _month, 1);
                addedDays = DaysUntilDayOfWeek(date, dayOfWeek, (int)schedule);
                break;
        }

        return date.AddDays(addedDays);
    }

    private static int DaysUntilDayOfWeek(DayOfWeek from, DayOfWeek target, int nth) => ((target - from + 7) % 7) + ((nth - 1) * 7);
    private static int DaysUntilDayOfWeek(DateTime from, DayOfWeek target, int nth) => DaysUntilDayOfWeek(from.DayOfWeek, target, nth);

    private static int DaysPassedFromDayOfWeek(DayOfWeek from, DayOfWeek target) => (from - target + 7) % 7;
    private static int DaysPassedFromDayOfWeek(DateTime from, DayOfWeek target) => DaysPassedFromDayOfWeek(from.DayOfWeek, target);
}

public enum Schedule
{
    Teenth,
    First,
    Second,
    Third,
    Fourth,
    Last
}