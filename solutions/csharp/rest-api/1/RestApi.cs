using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using RestApiExercise;
using RestApiExercise.Controllers;
using RestApiExercise.Data;
using RestApiExercise.Models.Dtos;
using RestApiExercise.Models.Entities;

public class RestApi
{
    private readonly Dictionary<string, Func<string, object>> _getMethods = new();
    private readonly Dictionary<string, Func<string, object>> _postMethods = new();

    private readonly IUserRepository _userRepository;

    public RestApi(string database)
    {
        JsonConvert.DefaultSettings = () => new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy(processDictionaryKeys: false, overrideSpecifiedNames: true)
            }
        };

        _userRepository = new UserRepository(database);
        InitializeControllers();
    }

    public string Get(string url, string payload = null) => ExecuteAction(_getMethods, url, payload);
    public string Post(string url, string payload) => ExecuteAction(_postMethods, url, payload);

    private static string ExecuteAction(Dictionary<string, Func<string, object>> actionSrc, string url, string payload) =>
        actionSrc.TryGetValue(url, out var action) ? JsonConvert.SerializeObject(action(payload)) : throw new InvalidOperationException("Invalid Url!");

    private void InitializeControllers()
    {
        var userController = new UserController(_userRepository);

        foreach (var methodInfo in userController.GetType().GetMethods())
        {
            var attr = methodInfo.GetCustomAttribute<ActionMethodAttribute>();
            if (attr is null)
                continue;

            var methodDict = attr.HttpMethod.Equals("post", StringComparison.InvariantCultureIgnoreCase) ? _postMethods : _getMethods;
            methodDict[attr.Route] = methodInfo.CreateDelegate<Func<string, object>>(userController);
        }
    }
}

namespace RestApiExercise
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class ActionMethodAttribute : Attribute
    {
        public ActionMethodAttribute(string httpMethod, string route)
        {
            HttpMethod = httpMethod;
            Route = route;
        }

        public string HttpMethod { get; }
        public string Route { get; }
    }
}

namespace RestApiExercise.Controllers
{
    internal class UserController
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository) => _repository = repository;

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
}

namespace RestApiExercise.Data
{
    internal interface IUserRepository
    {
        IReadOnlyCollection<User> GetUsers(IReadOnlyCollection<string> nameFilter = null);
        User AddUser(User user);
        void AddIOUInfo(string lenderName, string borrowerName, int amount);
    }

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
}

namespace RestApiExercise.Models.Dtos
{
    internal class CreateIOUDto
    {
        public string Lender { get; init; }
        public string Borrower { get; init; }
        public int Amount { get; init; }
    }

    internal class CreateUserDto
    {
        public string User { get; set; }

        public User ToEntity() => new()
        {
            Name = User
        };
    }

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
}

namespace RestApiExercise.Models.Entities
{
    internal class User
    {
        public string Name { get; init; }
        public SortedDictionary<string, int> Owes { get; } = new();
        public SortedDictionary<string, int> OwedBy { get; } = new();
        public int Balance => OwedBy.Values.Sum() - Owes.Values.Sum();
    }
}