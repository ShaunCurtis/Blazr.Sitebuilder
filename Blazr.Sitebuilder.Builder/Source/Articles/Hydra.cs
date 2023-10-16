using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Hydra")]
public class Hydra : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Blazor Hydra - Hosting Multiple Blazor SPAs on a single Site",
        PublishDate = new DateOnly(2021, 2, 26),
        LastUpdated = new(2021, 2, 26),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "How to build a ASPNetCore site that hosts Multiple Blazor Server and WASM SPA sites.",
        MdFile = $@".\Source\Articles\Hydra.md",
    };
}
