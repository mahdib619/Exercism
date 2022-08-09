using System.Collections.Generic;

public class FacialFeatures
{
    public string EyeColor { get; }
    public decimal PhiltrumWidth { get; }

    public FacialFeatures(string eyeColor, decimal philtrumWidth)
    {
        EyeColor = eyeColor;
        PhiltrumWidth = philtrumWidth;
    }

    public override bool Equals(object obj) => obj is FacialFeatures other && other.EyeColor == EyeColor && other.PhiltrumWidth == PhiltrumWidth;
    public override int GetHashCode() => $"{EyeColor}-{PhiltrumWidth}".GetHashCode();
}

public class Identity
{
    public string Email { get; }
    public FacialFeatures FacialFeatures { get; }

    public Identity(string email, FacialFeatures facialFeatures)
    {
        Email = email;
        FacialFeatures = facialFeatures;
    }

    public override bool Equals(object obj) => obj is Identity other && other.Email == Email && FacialFeatures.Equals(other.FacialFeatures);
    public override int GetHashCode() => $"{Email}-{FacialFeatures.GetHashCode()}".GetHashCode();
}

public class Authenticator
{
    private readonly Identity _admin = new("admin@exerc.ism", new FacialFeatures("green", 0.9m));
    private readonly HashSet<Identity> _identities = new();

    public static bool AreSameFace(FacialFeatures faceA, FacialFeatures faceB) => faceA.Equals(faceB);
    public bool IsAdmin(Identity identity) => identity.Equals(_admin);
    public bool Register(Identity identity) => _identities.Add(identity);
    public bool IsRegistered(Identity identity) => _identities.Contains(identity);
    public static bool AreSameObject(Identity identityA, Identity identityB) => identityA == identityB;
}
