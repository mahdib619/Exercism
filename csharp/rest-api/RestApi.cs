using System;
using System.Collections.Generic;
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using RestApiExercise;
using RestApiExercise.Controllers;
using RestApiExercise.Data;

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