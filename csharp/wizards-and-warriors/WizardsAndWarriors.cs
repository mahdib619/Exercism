abstract class Character
{
    private readonly string _characterType;

    protected Character(string characterType) => _characterType = characterType;

    public abstract int DamagePoints(Character target);
    public virtual bool Vulnerable() => false;
    public override string ToString() => $"Character is a {_characterType}";
}

class Warrior : Character
{
    public Warrior() : base("Warrior")
    {
    }

    public override int DamagePoints(Character target) => target.Vulnerable() ? 10 : 6;
}

class Wizard : Character
{
    private bool _spellIsPrepared;

    public Wizard() : base("Wizard")
    {
    }

    public override int DamagePoints(Character target) => _spellIsPrepared ? 12 : 3;
    public override bool Vulnerable() => !_spellIsPrepared;
    public void PrepareSpell() => _spellIsPrepared = true;
}
