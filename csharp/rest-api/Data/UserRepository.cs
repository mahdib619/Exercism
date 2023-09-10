using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestApiExercise.Models.Entities;

namespace RestApiExercise.Data;

internal class UserRepository : IUserRepository
{
    private readonly List<User> _users;

    public UserRepository(string data)
    {
        _users = JsonConvert.DeserializeObject<List<User>>(data);
    }

    public IReadOnlyCollection<User> GetUsers(IReadOnlyCollection<string> nameFilter = null)
    {
        return nameFilter is null ? _users : _users.Where(u => nameFilter.Contains(u.Name)).ToList();
    }

    public User AddUser(User user)
    {
        _users.Add(user);
        return user;
    }

    public void AddIOUInfo(string lenderName, string borrowerName, int amount)
    {
        var lender = _users.First(u => u.Name == lenderName);
        var borrower = _users.First(u => u.Name == borrowerName);

        if (!ApplyLenderOweAndCheckRemain(lender, borrower, ref amount))
            return;

        lender.OwedBy[borrowerName] = lender.OwedBy.GetValueOrDefault(borrowerName) + amount;
        borrower.Owes[lenderName] = lender.Owes.GetValueOrDefault(lenderName) + amount;
    }

    private static bool ApplyLenderOweAndCheckRemain(User lender, User borrower, ref int amount)
    {
        var lenderOwe = lender.Owes.GetValueOrDefault(borrower.Name);

        if (lenderOwe > 0)
        {
            var remain = Math.Max(lenderOwe - amount, 0);
            if (remain <= 0)
            {
                lender.Owes.Remove(borrower.Name);
                borrower.OwedBy.Remove(lender.Name);
            }
            else
            {
                borrower.OwedBy[lender.Name] = lender.Owes[borrower.Name] = remain;
            }

            amount = Math.Max(amount - lenderOwe, 0);
        }

        return amount > 0;
    }
}