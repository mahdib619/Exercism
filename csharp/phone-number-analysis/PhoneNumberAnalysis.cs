using System.Collections.Generic;

public static class PhoneNumber
{
    private static readonly List<string> _newyorkDialingCodes = new() { "212", "917", "646", "718" };

    public static (bool IsNewYork, bool IsFake, string LocalNumber) Analyze(string phoneNumber)
    {
        var parts = phoneNumber.Split('-');
        return (_newyorkDialingCodes.Contains(parts[0]), parts[1] is "555", parts[2]);
    }

    public static bool IsFake((bool IsNewYork, bool IsFake, string LocalNumber) phoneNumberInfo) => phoneNumberInfo.IsFake;
}
