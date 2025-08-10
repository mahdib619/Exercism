using System.Linq;
using System.Text.RegularExpressions;

public static class Luhn
{
    private static readonly Regex _regex = new(@"^(\d *){2,}$");
    public static bool IsValid(string number) => _regex.IsMatch(number) && number.Replace(" ", "").Reverse().Select(Selector).Sum() % 10 == 0;
    private static int Selector(char digit, int index) => index % 2 == 0 ? digit - '0' : (digit - '0') * 2 is var num && num > 9 ? num - 9 : num;
}