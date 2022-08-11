using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.RuntimeInformation;

public static class Appointment
{
    private static readonly Dictionary<Location, TimeZoneInfo> _timeZoneInfos;

    static Appointment()
    {
        var (nyId, londonId, parisId) = IsOSPlatform(OSPlatform.Windows) ? ("Eastern Standard Time", "GMT Standard Time", "W. Europe Standard Time") : ("America/New_York", "Europe/London", "Europe/Paris");

        _timeZoneInfos = new()
        {
            [Location.NewYork] = TimeZoneInfo.FindSystemTimeZoneById(nyId),
            [Location.London] = TimeZoneInfo.FindSystemTimeZoneById(londonId),
            [Location.Paris] = TimeZoneInfo.FindSystemTimeZoneById(parisId)
        };
    }

    public static DateTime ShowLocalTime(DateTime dtUtc) => dtUtc.ToLocalTime();

    public static DateTime Schedule(string appointmentDateDescription, Location location) => TimeZoneInfo.ConvertTimeToUtc(DateTime.Parse(appointmentDateDescription), _timeZoneInfos[location]);

    public static DateTime GetAlertTime(DateTime appointment, AlertLevel alertLevel) => alertLevel switch
    {
        AlertLevel.Early => appointment.AddDays(-1),
        AlertLevel.Standard => appointment.AddMinutes(-105),
        AlertLevel.Late => appointment.AddMinutes(-30),
        _ => throw new ArgumentException("Invalid Alert Level!", nameof(alertLevel))
    };

    public static bool HasDaylightSavingChanged(DateTime dt, Location location)
    {
        var tz = _timeZoneInfos[location];
        return tz.IsDaylightSavingTime(dt) != tz.IsDaylightSavingTime(dt.AddDays(-7));
    }

    public static DateTime NormalizeDateTime(string dtStr, Location location)
    {
        DateTime.TryParse(dtStr, location.GetCultureInfo(), DateTimeStyles.None, out var date);
        return date;
    }

    private static CultureInfo GetCultureInfo(this Location location) => location switch
    {
        Location.NewYork => new CultureInfo("en-US"),
        Location.London or Location.Paris => new CultureInfo("en-GB"),
        _ => null
    };
}

public enum Location
{
    NewYork,
    London,
    Paris
}

public enum AlertLevel
{
    Early,
    Standard,
    Late
}