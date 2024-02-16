using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Posts/Mongrel-Blazor")]
public class Mongrel_Blazor : PostsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Mongrel Blazor",
        PublishDate = new DateOnly(2023, 12, 28),
        LastUpdated = new(2024, 02, 10),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor; #Net8; #InteractiveAuto;",
        Description = "This post explains why Blazor's InteractiveAuto and Per Page/Component mode is a rocky road to choose for your first Blazor project.",
        MarkdownFile = "Mongrel-Blazor.md",
    };
}
