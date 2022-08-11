using System;

public static class PlayAnalyzer
{
    public static string AnalyzeOnField(int shirtNum) => shirtNum switch
    {
        1 => "goalie",
        2 => "left back",
        3 or 4 => "center back",
        5 => "right back",
        6 or 7 or 8 => "midfielder",
        9 => "left wing",
        10 => "striker",
        11 => "right wing",
        _ => throw new ArgumentOutOfRangeException(nameof(shirtNum))
    };

    public static string AnalyzeOffField(object report) => report switch
    {
        int supportersCount => $"There are {supportersCount} supporters at the match.",
        string reportMessage => reportMessage,
        Manager manager => $"{manager.Name}{(manager.Club is null ? "" : $" ({manager.Club})")}",
        Injury injury => $"Oh no! {injury.GetDescription()} Medics are on the field.",
        Incident incident => incident.GetDescription(),
        _ => throw new ArgumentException("Invalid report", nameof(report))
    };
}
