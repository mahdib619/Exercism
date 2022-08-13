using System.Text;

public static class RotationalCipher
{
    public static string Rotate(string text, int shiftKey)
    {
        var rotated = new StringBuilder();

        foreach (var ch in text)
            rotated.Append(Rotate(ch, shiftKey));

        return rotated.ToString();
    }

    public static char Rotate(char ch, int shiftKey) => char.IsLetter(ch) ? (char)Rotate((int)ch, shiftKey) : ch;

    public static int Rotate(int ascii, int shifKey)
    {
        var start = ascii >= 97 ? 97 : 65;
        return (((ascii + shifKey) % start) % 26) + start;
    }
}