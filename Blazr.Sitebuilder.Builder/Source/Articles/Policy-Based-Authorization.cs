using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Policy-Based-Authorization")]
public class Policy_Based_Authorization : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Policy Base Authorization in Blazor",
        PublishDate = new DateOnly(2021, 2, 26),
        LastUpdated = new(2021, 2, 26),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;#Auth;",
        Description = "This article describes how to implement policy based authentication in blazor Applications.",
        MarkdownFile = "Policy-Based-Authorization.md",
    };
}
