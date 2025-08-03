using System;
using System.Text.RegularExpressions;

public class PhoneNumber
{
    public static string Clean(string phoneNumber)
    {
        if (!new Regex(@"^ *\+?1? *([ \(-\.]*[2-9]\d\d[ \)-\.]*){2}\d{4} *$").IsMatch(phoneNumber))
            throw new ArgumentException("Invalid phone number!", nameof(phoneNumber));

        return new Regex(@"(?!\d).").Replace(phoneNumber, "")[^10..];
    }
}