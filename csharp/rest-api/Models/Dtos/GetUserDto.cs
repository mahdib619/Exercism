using Newtonsoft.Json;
using RestApiExercise.Models.Entities;
using System.Collections.Generic;

namespace RestApiExercise.Models.Dtos;

internal class GetUserDto
{
    public string Name { get; set; }
    public IReadOnlyDictionary<string, int> Owes { get; private set; }

    [JsonProperty(PropertyName = "owed_by")]
    public IReadOnlyDictionary<string, int> OwedBy { get; private set; }

    public int Balance { get; private set; }

    public static GetUserDto FromEntity(User user) => new()
    {
        Name = user.Name,
        Owes = user.Owes,
        OwedBy = user.OwedBy,
        Balance = user.Balance
    };
}