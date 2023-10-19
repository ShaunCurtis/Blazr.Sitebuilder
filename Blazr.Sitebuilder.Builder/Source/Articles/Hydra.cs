using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Hydra")]
public class Hydra : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Blazor Hydra - Hosting Multiple Blazor SPAs on a single Site",
        PublishDate = new DateOnly(2021, 2, 26),
        LastUpdated = new(2021, 2, 26),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "How to build a ASPNetCore site that hosts Multiple Blazor Server and WASM SPA sites.",
        MarkdownFile = "Hydra.md",
    };
}
