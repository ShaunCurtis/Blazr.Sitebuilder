using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Blazor-CSS")]
public class Blazor_CSS : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Working with CSS in Blazor",
        PublishDate = new DateOnly(2020, 10, 1),
        LastUpdated = new(2020, 10, 1),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "This article describes how to customize the default CSS setup in Blazor, and looks at the new Scoped CSS.",
        MdFile = $@".\Source\Articles\Blazor-CSS.md",
    };
}
