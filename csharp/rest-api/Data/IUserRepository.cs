using System.Collections.Generic;
using RestApiExercise.Models.Entities;

namespace RestApiExercise.Data;

internal interface IUserRepository
{
    IReadOnlyCollection<User> GetUsers(IReadOnlyCollection<string> nameFilter = null);
    User AddUser(User user);
    void AddIOUInfo(string lenderName, string borrowerName, int amount);
}