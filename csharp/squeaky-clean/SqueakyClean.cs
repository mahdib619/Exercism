using System.Text;

public static class Identifier
{
    public static string Clean(string identifier)
    {
        var idBuilder = new StringBuilder();
        var toUpperHandler = 2; //char will be uppercase if this var is 1

        foreach (char c in identifier)
        {
            var cleanedChar = "";

            if (char.IsLetter(c) && !c.IsLowerGreek())
                cleanedChar = (toUpperHandler == 1 ? char.ToUpper(c) : c).ToString();
            else if (char.IsControl(c))
                cleanedChar = "CTRL";
            else if (c == ' ')
                cleanedChar = "_";
            else if (c == '-')
                toUpperHandler = 0;

            toUpperHandler++;
            idBuilder.Append(cleanedChar);
        }

        return idBuilder.ToString();
    }

    private static bool IsLowerGreek(this char c) => (int)c is >= 945 and <= 969;
}
