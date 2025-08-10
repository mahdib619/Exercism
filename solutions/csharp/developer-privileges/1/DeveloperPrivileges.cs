using System.Collections.Generic;

public class Authenticator
{
    private Identity _admin;
    public Identity Admin => _admin ??= new()
    {
        Email = "admin@ex.ism",
        FacialFeatures = new()
        {
            EyeColor = "green",
            PhiltrumWidth = 0.9m
        },
        NameAndAddress = new List<string> { "Chanakya", "Mumbai", "India" }
    };

    private IDictionary<string, Identity> _developers;
    public IDictionary<string, Identity> Developers => _developers ??= new Dictionary<string, Identity>
    {
        ["Bertrand"] = new()
        {
            Email = "bert@ex.ism",
            FacialFeatures = new()
            {
                EyeColor = "blue",
                PhiltrumWidth = 0.8m
            },
            NameAndAddress = new List<string> { "Bertrand", "Paris", "France" }
        },
        ["Anders"] = new()
        {
            Email = "anders@ex.ism",
            FacialFeatures = new()
            {
                EyeColor = "brown",
                PhiltrumWidth = 0.85m
            },
            NameAndAddress = new List<string> { "Anders", "Redmond", "USA" }
        }
    };
}

//**** please do not modify the FacialFeatures class ****
public class FacialFeatures
{
    public string EyeColor { get; set; }
    public decimal PhiltrumWidth { get; set; }
}

//**** please do not modify the Identity class ****
public class Identity
{
    public string Email { get; set; }
    public FacialFeatures FacialFeatures { get; set; }
    public IList<string> NameAndAddress { get; set; }
}
