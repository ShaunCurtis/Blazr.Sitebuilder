/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

using System.Collections.ObjectModel;
using System.Reflection;

namespace Blazr.SiteBuilder;

public record SiteRouteData(Type Component, string Route, PageData PageData);

public class RouteFactory
{
    public IEnumerable<SiteRouteData> RouteList => _routeList.AsEnumerable();
    public ReadOnlyDictionary<string, int> Categories => _categories.AsReadOnly();
    public IEnumerable<string> Tags => _tags.AsEnumerable();

    private List<SiteRouteData> _routeList = new();
    private Dictionary<string, int> _categories = new();
    private List<string> _tags = new();

    public RouteFactory()
    {
        GetRoutes();
    }

    private void GetRoutes()
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
                        this.AddTags(contentComponent.PageData.Tags);
                    }
                }
            }
        }

        //Get the categories 
        var categories = _routeList.Select(item => item.PageData.Category).Distinct().ToList();
        foreach(var category in categories)
        {
            _categories.Add(category, _routeList.Where(item => item.PageData.Category == category).Count());
        }
    }

    private void AddTags(string tags)
    {
        var tagList = tags.Split(";");
        foreach (var tag in tagList)
        {
            if (!_tags.Contains(tag))
                _tags.Add(tag);
        }
    }
}

