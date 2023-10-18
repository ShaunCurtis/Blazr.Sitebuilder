using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Blazor-CSS")]
public class Blazor_CSS : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Working with CSS in Blazor",
        PublishDate = new DateOnly(2020, 10, 1),
        LastUpdated = new(2020, 10, 1),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "This article describes how to customize the default CSS setup in Blazor, and looks at the new Scoped CSS.",
        MarkdownFile = $@"Articles\Blazor-CSS.md",
    };
}
