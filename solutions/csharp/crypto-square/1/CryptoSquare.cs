using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class CryptoSquare
{
    public static string Ciphertext(string plaintext)
    {
        if (string.IsNullOrEmpty(plaintext))
            return plaintext;

        var encoded = Encoded(plaintext);
        var (r, _) = GetColAndRow(encoded);

        return string.Join(" ", encoded.ChunckString(r));
    }

    public static string Encoded(string plaintext)
    {
        var segments = PlaintextSegments(plaintext, out var nRow, out var nCol);
        var encoded = Enumerable.Repeat(' ', nRow * nCol).ToArray();

        foreach (var (segment, r) in segments.Select((i, s) => (i, s)))
        {
            for (var c = 0; c < nCol; c++)
                encoded[c * nRow + r] = segment[c];
        }

        return string.Join("", encoded);
    }

    public static IEnumerable<string> PlaintextSegments(string plaintext, out int row, out int col)
    {
        var nText = NormalizedPlaintext(plaintext);
        (row, col) = GetColAndRow(nText);
        return nText.ChunckString(col);
    }

    public static string NormalizedPlaintext(string plaintext) => new Regex("\\W").Replace(plaintext, "").ToLower();

    private static (int r, int c) GetColAndRow(string str)
    {
        var length = str.Length;
        var r = (int)Math.Round(Math.Sqrt(length));
        return r * r >= length ? (r, r) : (r, r + 1);
    }

    private static IEnumerable<string> ChunckString(this string str, int chunckLength) => str.Chunk(chunckLength).Select(chs => string.Join("", chs).PadRight(chunckLength));
}