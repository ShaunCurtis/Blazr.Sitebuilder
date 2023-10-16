using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Policy-Based-Authorization")]
public class Policy_Based_Authorization : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Policy Base Authorization in Blazor",
        PublishDate = new DateOnly(2021, 2, 26),
        LastUpdated = new(2021, 2, 26),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;#Auth;",
        Description = "This article describes how to implement policy based authentication in blazor Applications.",
        MdFile = $@".\Source\Articles\Policy-Based-Authorization.md",
    };
}
