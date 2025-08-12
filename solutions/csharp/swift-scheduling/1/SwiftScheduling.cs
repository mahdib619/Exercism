public static class SwiftScheduling
{
    public static DateTime DeliveryDate(DateTime meetingStart, string description) => (meetingStart, description) switch
    {
        (_, "NOW") => meetingStart.AddHours(2),
        ({ Hour: < 13 }, "ASAP") => meetingStart.Date.AddHours(17),
        ({ Hour: >= 13 }, "ASAP") => meetingStart.Date.AddHours(37),
        ({ DayOfWeek: DayOfWeek.Monday or DayOfWeek.Tuesday or DayOfWeek.Wednesday }, "EOW") => meetingStart.Date.AddHours((DayOfWeek.Friday - meetingStart.DayOfWeek) * 24 + 17),
        (_, "EOW") => meetingStart.Date.AddHours((7 - (int)meetingStart.DayOfWeek) * 24 + 20),
        (_, [.. var monthStr, 'M']) when int.Parse(monthStr) is var month && new DateTime(meetingStart.Year + (meetingStart.Month < month ? 0 : 1), month, 1) is var date => date.AddHours((date.DaysTillWeekday()) * 24 + 8),
        (_, ['Q', .. var quarterStr]) when int.Parse(quarterStr) * 3 is var month && meetingStart.Year + (meetingStart.Month <= month ? 0 : 1) is var year && new DateTime(year, month, DateTime.DaysInMonth(year, month)) is var date => date.AddHours((date.DaysTillWeekdayReverse()) * 24 + 8),
        _ => new(2000, 12, 27)
    };

    private static int DaysTillWeekday(this DateTime dateTime) => dateTime.DayOfWeek switch
    {
        DayOfWeek.Saturday => 2,
        DayOfWeek.Sunday => 1,
        _ => 0
    };
    
    private static int DaysTillWeekdayReverse(this DateTime dateTime) => dateTime.DayOfWeek switch
    {
        DayOfWeek.Saturday => -1,
        DayOfWeek.Sunday => -2,
        _ => 0
    };
}