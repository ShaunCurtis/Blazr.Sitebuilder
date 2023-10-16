using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Blazor-Components")]
public class Blazor_Components : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "A Deep Dive into Components",
        PublishDate = new DateOnly(2020, 11, 11),
        LastUpdated = new(2021, 2, 16),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "This article take a detailed look at the anatomy of a component and how it interacts with the rendering process.",
        MdFile = $@".\Source\Articles\Blazor-Components.md",
    };
}
