using System;
using System.Linq;

public class DndCharacter
{
    private DndCharacter()
    {
    }

    public int Strength { get; init; }
    public int Dexterity { get; init; }
    public int Constitution { get; init; }
    public int Intelligence { get; init; }
    public int Wisdom { get; init; }
    public int Charisma { get; init; }
    public int Hitpoints { get; set; }

    public static int Modifier(int score) => (int)Math.Floor((score - 10) / 2d);

    public static int Ability() => Enumerable.Range(0, 4).Select(_ => Random.Shared.Next(1, 7)).OrderDescending().Take(3).Sum();

    public static DndCharacter Generate()
    {
        var character = new DndCharacter
        {
            Strength = Ability(),
            Dexterity = Ability(),
            Constitution = Ability(),
            Intelligence = Ability(),
            Wisdom = Ability(),
            Charisma = Ability()
        };

        character.Hitpoints = 10 + Modifier(character.Constitution);
        return character;
    }
}
