using System.Linq;

public static class Bob
{
    public static string Response(string statement)
    {
        if (string.IsNullOrWhiteSpace(statement))
            return "Fine. Be that way!";

        statement = statement.TrimEnd();

        var hasLetter = false;
        var noLower = statement.All(c =>
        {
            if (!char.IsLetter(c))
                return true;

            hasLetter = true;
            return char.IsUpper(c);
        });
        var allUpper = hasLetter && noLower;

        return statement[^1] == '?' ? allUpper ? "Calm down, I know what I'm doing!" : "Sure." : allUpper ? "Whoa, chill out!" : "Whatever.";
    }
}