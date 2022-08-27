using System.Collections.Generic;
using System.Linq;

public static class ProteinTranslation
{
    public static string[] Proteins(string strand)
    {
        var proteins = new LinkedList<string>();

        for (var i = 0; i < strand.Length; i += 3)
        {
            var protein = GetProtein(strand[i..(i + 3)]);

            if (protein == "STOP")
                break;

            proteins.AddLast(protein);
        }

        return proteins.ToArray();
    }

    private static string GetProtein(string codon) => codon switch
    {
        "AUG" => "Methionine",
        "UUU" or "UUC" => "Phenylalanine",
        "UUA" or "UUG" => "Leucine",
        "UCU" or "UCC" or "UCA" or "UCG" => "Serine",
        "UAU" or "UAC" => "Tyrosine",
        "UGU" or "UGC" => "Cysteine",
        "UGG" => "Tryptophan",
        "UAA" or "UAG" or "UGA" => "STOP",
        _ => null
    };
}