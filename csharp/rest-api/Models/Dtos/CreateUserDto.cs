using RestApiExercise.Models.Entities;

namespace RestApiExercise.Models.Dtos;

internal class CreateUserDto
{
    public string User { get; set; }

    public User ToEntity() => new()
    {
        Name = User
    };
}