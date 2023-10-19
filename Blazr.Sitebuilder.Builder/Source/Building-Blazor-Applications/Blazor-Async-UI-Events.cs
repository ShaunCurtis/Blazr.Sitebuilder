using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/Blazor-Async-UI-Events")]
public class Blazor_Async_UI_Events : BuildingBlazorApplicationsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Blazor Async Behaviour on UI Events",
        PublishDate = new DateOnly(2020, 10, 1),
        LastUpdated = new(2020, 10, 1),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;",
        Description = "What Async patterns to use for UI events in Blazor.",
        MarkdownFile = $@"Blazor-Async-UI-Events.md",
        HideInNavigationLists = false,
    };
}
