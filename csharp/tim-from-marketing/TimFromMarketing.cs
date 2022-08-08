using System;

static class Badge
{
    public static string Print(int? id, string name, string? department)
    {
        var idStr = id is not null ? $"[{id}] - " : "";
        var departementStr = department?.ToUpper() ?? "OWNER";

        return $"{idStr}{name} - {departementStr}";
    }
}
