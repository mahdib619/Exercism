using System;
using System.Collections.Generic;
using System.Linq;

public static class Proverb
{
    public static string[] Recite(string[] subjects)
    {
        if (subjects.Length == 0)
            return Array.Empty<string>();

        var output = new LinkedList<string>();

        for (var i = 1; i < subjects.Length; i++)
            output.AddLast($"For want of a {subjects[i - 1]} the {subjects[i]} was lost.");

        output.AddLast($"And all for the want of a {subjects[0]}.");

        return output.ToArray();
    }
}