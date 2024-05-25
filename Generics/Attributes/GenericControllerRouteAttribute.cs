using Generics.Entities;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Generics.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class GenericControllerRouteAttribute : Attribute
{
    public GenericControllerRouteAttribute(string route)
    {
        Route = route;
    }

    public string Route { get; }
}
