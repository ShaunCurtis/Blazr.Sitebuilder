/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

using Microsoft.Extensions.DependencyInjection;

namespace Blazr.SiteBuilder;

public class SiteBuilderFactory
{
    private IServiceProvider _serviceProvider;
    private readonly ContentRenderer _contentRenderer;

    public SiteBuilderFactory(IServiceProvider serviceProvider, ContentRenderer contentRenderer)
    {
        _contentRenderer = contentRenderer;
        _serviceProvider = serviceProvider;
    }

    public async Task BuildSiteAsync()
    {
        await BuildRoutesAsync();
    }

    public async Task BuildRoutesAsync()
    {
        foreach (var route in _contentRenderer.RouteProvider.RouteList)
        {
            await _contentRenderer.SetCurrentRouteAsync(route);
 
            using var htmlRenderer = ActivatorUtilities.CreateInstance<HtmlRenderer>(_serviceProvider);

            var html = await htmlRenderer.Dispatcher.InvokeAsync(async () =>
            {
                HtmlRootComponent output = await htmlRenderer.RenderComponentAsync(route.Component);
                return output.ToHtmlString();
            });

            string htmlFlePath = $"{Environment.CurrentDirectory}/wwwroot{route.Route}.html";

            // Create path if it doesn't exist               
            var folder = Path.GetDirectoryName(htmlFlePath);

            if (folder is not null && !Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            // Write file
            await File.WriteAllTextAsync(htmlFlePath, html);
        }
    }
}
