using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Posts/Blazor-Loading-Overlay")]
public class Blazor_Loading_Overlay : PostsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Blazor Loading Overlay",
        PublishDate = new DateOnly(2023, 10, 30),
        LastUpdated = new(2023, 10, 30),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;",
        Description = "This post shows how to build a simple overlay you can apply to any page while the page is loading.",
        MarkdownFile = "Blazor-Loading-Overlay.md",
    };
}
