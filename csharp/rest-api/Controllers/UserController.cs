using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestApiExercise.Data;
using RestApiExercise.Models.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace RestApiExercise.Controllers;

internal class UserController
{
    private readonly IUserRepository _repository;

    public UserController(IUserRepository repository)
    {
        _repository = repository;
    }

    [ActionMethod("Get", "/users")]
    public IReadOnlyCollection<GetUserDto> GetUsers(string payload = null)
    {
        var names = payload is null ? null : (JsonConvert.DeserializeObject<JObject>(payload)["users"] as JArray)?.Select(tkn => tkn.ToObject<string>()).ToList();
        return _repository.GetUsers(names).Select(GetUserDto.FromEntity).ToList();
    }

    [ActionMethod("Post", "/add")]
    public GetUserDto AddUser(string user)
    {
        var userObject = JsonConvert.DeserializeObject<CreateUserDto>(user).ToEntity();
        var result = _repository.AddUser(userObject);
        return GetUserDto.FromEntity(result);
    }

    [ActionMethod("Post", "/iou")]
    public IReadOnlyCollection<GetUserDto> AddIOU(string iou)
    {
        var iouObject = JsonConvert.DeserializeObject<CreateIOUDto>(iou);
        _repository.AddIOUInfo(iouObject.Lender, iouObject.Borrower, iouObject.Amount);
        return GetUsers(JsonConvert.SerializeObject(new { Users = new[] { iouObject.Borrower, iouObject.Lender } }));
    }
}