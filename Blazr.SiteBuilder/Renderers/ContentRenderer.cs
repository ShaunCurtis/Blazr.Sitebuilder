/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
using Markdig;
using System.Diagnostics;

namespace Blazr.SiteBuilder;

public class ContentRenderer
{
    private string? _articleHtml;
    private string? _TOCHtml;

    public readonly RouteProvider RouteProvider;
    public readonly StoryProvider StoryProvider;
    public readonly ISiteData SiteData;
    public MarkupString ArticleContent => new MarkupString(_articleHtml ?? string.Empty);
    public MarkupString TOCContent => new MarkupString(_TOCHtml ?? string.Empty);
    public SiteRouteData CurrentRoute { get; private set; }

    public IPageData PageData => this.CurrentRoute.PageData;

    public ContentRenderer(RouteProvider routeProvider, StoryProvider storyProvider, ISiteData siteData)
    {
        RouteProvider = routeProvider;
        StoryProvider = storyProvider;
        SiteData = siteData;

        // Get the default route 
        var defaultRoute = routeProvider.RouteList.FirstOrDefault(item => item.PageData.DefaultRoute);
        Debug.Assert(defaultRoute != null);
        this.CurrentRoute = defaultRoute;
    }

    public async Task SetCurrentRouteAsync(SiteRouteData route)
    {
        this.CurrentRoute = route;
        if (route.PageData.MdFile is not null)
            await GetContentAsMarkupString();
    }


    protected async Task GetContentAsMarkupString()
    {
        // Get the Markdown text from the file
        string markdownFlePath = Environment.CurrentDirectory + PageData.MdFile;
        Debug.Assert(File.Exists(markdownFlePath));
        string markdownText = await File.ReadAllTextAsync(markdownFlePath);

        // Process the Markdown to html
        var markdownPipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
        _articleHtml = Markdown.ToHtml(markdownText, markdownPipeline);

        // Gwt the TOC
        var toc = TOCUtils.GetTOC(_articleHtml);
        _TOCHtml = TOCBuilder.BuildTOC(toc);
    }
}