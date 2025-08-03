using System;
using System.Linq;

public static class AffineCipher
{
    private const int M = 26;
    private const int GroupSize = 5;

    public static string Encode(string plainText, int a, int b)
    {
        CheckCoPrime(a);

        plainText = plainText.ToLower();
        var chipherdTextGroups = plainText.Select(ch => Encode(ch, a, b))
                                          .Where(ch => ch.HasValue)
                                          .Chunk(GroupSize)
                                          .Select(chunk => string.Join("", chunk));

        return string.Join(" ", chipherdTextGroups);
    }

    private static char? Encode(char plainChar, int a, int b)
    {
        if (char.IsDigit(plainChar))
            return plainChar;
        if (!char.IsLetter(plainChar))
            return null;

        var i = plainChar - 'a';
        var cipherdIndex = (a * i + b) % M;

        return (char)(cipherdIndex + 'a');
    }

    public static string Decode(string cipheredText, int a, int b)
    {
        CheckCoPrime(a);

        var mmi = MMI(a);
        var plainChars = cipheredText.Select(ch => Decode(ch, b, mmi))
                                     .Where(ch => ch.HasValue);

        return string.Join("", plainChars);
    }

    private static char? Decode(char cipheredChar, int b, int mmi)
    {
        if (char.IsDigit(cipheredChar))
            return cipheredChar;
        if (!char.IsLetter(cipheredChar))
            return null;

        var y = cipheredChar - 'a';

        var plainIndex = mmi * (y - b) % M;
        var plainChar = (char)(plainIndex + 'a');

        return plainIndex < 0 ? (char)(plainChar + M) : plainChar;
    }

    private static void CheckCoPrime(int a)
    {
        if (GCD(a, M) != 1)
            throw new ArgumentException("Argument is not coprime to m!", nameof(a));
    }

    private static int MMI(int a)
    {
        for (var i = M + 1; ; i += M)
        {
            if (i % a == 0)
                return i / a;
        }
    }

    private static int GCD(int a, int b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }
}
