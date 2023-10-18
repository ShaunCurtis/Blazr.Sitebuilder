using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Blazor-AllinOne")]
public class Blazor_AllinOne : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Blazor All In One - Multi SPA Hosting",
        PublishDate = new DateOnly(2021, 2, 26),
        LastUpdated = new(2021, 4, 4),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "How to build a single Blazor Application that runs in both WASM and Server Modes.",
        MarkdownFile = $"Blazor-AllinOne.md",
    };
}
