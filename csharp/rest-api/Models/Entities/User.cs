using System.Collections.Generic;
using System.Linq;

namespace RestApiExercise.Models.Entities;

internal class User
{
    public string Name { get; init; }
    public SortedDictionary<string, int> Owes { get; } = new();
    public SortedDictionary<string, int> OwedBy { get; } = new();
    public int Balance => OwedBy.Values.Sum() - Owes.Values.Sum();
}