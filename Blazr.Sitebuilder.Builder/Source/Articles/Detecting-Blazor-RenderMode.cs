using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Detecting-Blazor-RenderMode")]
public class Detecting_Blazor_RenderMode : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Detecting Blazor's RenderMode",
        PublishDate = new DateOnly(2023, 12, 14),
        LastUpdated = new(2023, 12, 14),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "Net8.0 introduced RenderNode.  Great, but how do you detect which mode you're in.",
        MarkdownFile = "Detecting-Blazor-RenderMode.md",
    };
}
