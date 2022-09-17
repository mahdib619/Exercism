using System.Linq;

public static class AtbashCipher
{
    public static string Encode(string plainValue) => DoCipher(plainValue, 5);

    public static string Decode(string encodedValue) => DoCipher(encodedValue, encodedValue.Length);

    public static char DoCipher(char ch) => ch switch
    {
        _ when char.IsDigit(ch) => ch,
        _ when char.IsLetter(ch) => (char)(219 - char.ToLower(ch)),
        _ => default
    };

    public static string DoCipher(string str, int chunck) => string.Join(' ',
        str.Select(DoCipher)
           .Where(ch => ch != default)
           .Chunk(chunck)
           .Select(ch => string.Join("", ch)));
}
