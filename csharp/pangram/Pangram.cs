using System.Linq;

public static class Pangram
{
    public static bool IsPangram(string input) => input.Where(char.IsLetter).Select(char.ToUpper).Distinct().Count() == 26;
}
