/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;

namespace Blazr.SiteBuilder;

public record SiteRouteData(Type Component, string Route, PageData PageData);

public class RouteProvider
{
    public IEnumerable<SiteRouteData> RouteList => _routeList.AsEnumerable();
    public ReadOnlyDictionary<string, int> Categories => _categories.AsReadOnly();
    public IEnumerable<string> Tags => _tags.AsEnumerable();
    public SiteRouteData CurrentRoute {  get; private set; }

    private List<SiteRouteData> _routeList = new();
    private Dictionary<string, int> _categories = new();
    private List<string> _tags = new();
    public ISiteData SiteData { get; init; }

    public RouteProvider(ISiteData siteData)
    {
        this.SiteData = siteData;
        GetRoutes();

        var defaultRoute = _routeList.FirstOrDefault(item => item.PageData.DefaultRoute);
        Debug.Assert(defaultRoute != null);
        this.CurrentRoute = defaultRoute;
    }

    public void SetCurrentRoute(SiteRouteData route)
        => this.CurrentRoute = route;

    public IEnumerable<SiteRouteData> RouteListForCategory(string category) 
        => _routeList
        .Where(item => !item.PageData.HideInNavigationLists && item.PageData.Category.Equals(category, StringComparison.CurrentCultureIgnoreCase))
        .OrderByDescending(item => item.PageData.LastUpdated)
        .AsEnumerable();

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
            if (!string.IsNullOrWhiteSpace(category))
            {
                _categories.Add(category, _routeList.Where(item => item.PageData.Category == category && !item.PageData.HideInNavigationLists).Count());
            }
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

