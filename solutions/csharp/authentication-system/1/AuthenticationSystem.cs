using System.Collections.Generic;

public class Authenticator
{
    public Authenticator(Identity admin) => Admin = admin;

    private readonly Dictionary<string, Identity> developers = new()
    {
        ["Bertrand"] = new Identity
        {
            Email = "bert@ex.ism",
            EyeColor = "blue"
        },
        ["Anders"] = new Identity
        {
            Email = "anders@ex.ism",
            EyeColor = "brown"
        }
    };

    public Identity Admin { get; }

    public IReadOnlyDictionary<string, Identity> GetDevelopers() => developers;

    private class EyeColor
    {
        public string Blue = "blue";
        public string Green = "green";
        public string Brown = "brown";
        public string Hazel = "hazel";
        public string Brey = "grey";
    }
}

public readonly struct Identity
{
    public string Email { get; init; }
    public string EyeColor { get; init; }
}
