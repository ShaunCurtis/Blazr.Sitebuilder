/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

namespace Blazr.SiteBuilder;

public class StoryFactory
{
    private RouteFactory _routeFactory;
    private List<StoryData> _stories;

    public IEnumerable<StoryData> Stories => _stories.AsEnumerable();

    public StoryFactory(RouteFactory routeFactory)
    {
        _routeFactory = routeFactory;

        var storyArticles = _routeFactory.RouteList.Where(item => item.PageData.Category.Equals("Stories", StringComparison.CurrentCultureIgnoreCase));

        _stories = storyArticles.Select(item => item.PageData.Story).Distinct().Select(item => new StoryData(item)).ToList();
    }

    public IEnumerable<SiteRouteData> GetStoryArticles(string story)
        => _routeFactory.RouteList
        .Where(item => item.PageData.Story.Equals(story, StringComparison.CurrentCultureIgnoreCase))
        .OrderBy(item => item.PageData.Order);

}

