/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

using System.Reflection;

namespace Blazr.SiteBuilder;

public record SiteRouteData(Type Component, string Route, PageData PageData);

public class RouteFactory
{
    public IEnumerable<SiteRouteData> RouteList => _routeList.AsEnumerable();

    private List<SiteRouteData> _routeList = new();


    public void GetRoutes()
    {
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                var attribs = type.GetCustomAttributes(typeof(RouteAttribute), false);
                if (attribs != null && attribs.Length > 0)
                {
                    var instance = Activator.CreateInstance(type);

                    if (instance is IContentComponent contentComponent)
                    {
                        var route = type.GetCustomAttribute<RouteAttribute>()?.Template;
                        _routeList.Add(new(type, route ?? string.Empty, contentComponent.PageData));
                    }
                }
            }
        }
    }
}

public static class AttributeExtensions
{
    //public static TValue? GetAttributeValue<TAttribute, TValue>(
    //    this Type type,
    //    Func<TAttribute, TValue> valueSelector)
    //    where TAttribute : Attribute
    //{
    //    var att = type.GetCustomAttributes(
    //        typeof(TAttribute), true
    //    ).FirstOrDefault() as TAttribute;
    //    if (att != null)
    //    {
    //        return valueSelector(att);
    //    }
    //    return default(TValue);
    //}
}

