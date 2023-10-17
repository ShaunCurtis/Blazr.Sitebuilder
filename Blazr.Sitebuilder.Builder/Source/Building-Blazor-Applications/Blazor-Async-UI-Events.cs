using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/Blazor-Async-UI-Events")]
public class Blazor_Async_UI_Events : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Blazor Async Behaviour on UI Events",
        PublishDate = new DateOnly(2020, 10, 1),
        LastUpdated = new(2020, 10, 1),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;",
        Description = "What Async patterns to use for UI events in Blazor.",
        MdFile = $@".\Source\Building-Blazor-Applications\Blazor-Async-UI-Events.md",
    };
}
