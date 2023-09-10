using System;

namespace RestApiExercise;

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