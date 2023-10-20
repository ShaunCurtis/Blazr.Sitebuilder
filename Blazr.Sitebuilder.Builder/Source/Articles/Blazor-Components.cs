using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Blazor-Components")]
public class Blazor_Components : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "A Deep Dive into Components",
        PublishDate = new DateOnly(2020, 11, 11),
        LastUpdated = new(2021, 2, 16),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "This article take a detailed look at the anatomy of a component and how it interacts with the rendering process.",
        MarkdownFile = $@"Blazor-Components.md",
    };
}
