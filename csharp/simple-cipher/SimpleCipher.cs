using System;
using System.Linq;
using System.Text;

public class SimpleCipher
{
    public string Key { get; }

    public SimpleCipher(string key) => Key = key;

    public SimpleCipher()
    {
        var rnd = new Random();
        Key = string.Join("", Enumerable.Range(1, 100).Select(_ => (char)rnd.Next('a', 'z' + 1)));
    }

    public string Encode(string plaintext) => Shift(plaintext, ShiftUp);
    public string Decode(string ciphertext) => Shift(ciphertext, ShiftDown);

    private string Shift(string input, Func<char, char, char> charShifter)
    {
        var output = new StringBuilder();
        for (var i = 0; i < input.Length; i++)
        {
            var (ch, shift) = (input[i], Key[i % Key.Length]);
            output.Append(charShifter(ch, shift));
        }
        return output.ToString();
    }

    private char ShiftUp(char ch, char shift) => (char)('a' + (((ch + shift) % 'a') % 26));
    private char ShiftDown(char ch, char shift) => (char)('a' + ((ch - shift + 26) % 26));
}