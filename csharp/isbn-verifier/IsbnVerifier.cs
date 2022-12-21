using System.Linq;
using System.Text.RegularExpressions;

public static class IsbnVerifier
{
    private static readonly Regex _regex = new(@"^\d{9}(\d|X)$");

    public static bool IsValid(string number)
    {
        number = number.Replace("-", string.Empty);
        if (!_regex.IsMatch(number))
            return false;

        var check = number[^1];
        var intValue = number[..^1].Select((ch, i) => ch.ToInt() * (10 - i)).Sum() + (check == 'X' ? 10 : check.ToInt());

        return intValue % 11 == 0;
    }

    private static int ToInt(this char digit) => digit - '0';
}