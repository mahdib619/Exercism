using System;
using System.Collections.Specialized;
using System.Linq;

public class Allergies
{
    private readonly BitVector32 _levelBits;

    public Allergies(int mask) => _levelBits = new(mask);

    public bool IsAllergicTo(Allergen allergen) => _levelBits[(int)allergen];

    public Allergen[] List() => Enum.GetValues<Allergen>().Where(a => _levelBits[(int)a]).ToArray();
}

public enum Allergen
{
    Eggs = 1,
    Peanuts = 2,
    Shellfish = 4,
    Strawberries = 8,
    Tomatoes = 16,
    Chocolate = 32,
    Pollen = 64,
    Cats = 128
}