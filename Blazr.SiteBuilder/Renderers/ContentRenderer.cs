/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
namespace Blazr.SiteBuilder;

public class ContentRenderer
{
    private readonly RouteProvider _routeProvider;

    public ContentRenderer(RouteProvider routeProvider)
    {
        _routeProvider = routeProvider;
    }

    public string Render(HtmlFragment fragment)
    {
        return fragment(_routeProvider);
    }

}