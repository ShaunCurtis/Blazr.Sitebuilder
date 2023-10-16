using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Building-Edit-Forms")]
public class Building_Edit_Forms : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Managing Form Edit State in Blazor",
        PublishDate = new DateOnly(2021, 2, 8),
        LastUpdated = new(2021, 2, 8),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "How to build Blazor Edit Forms that manage state.",
        MdFile = $@".\Source\Articles\Building-Edit-Forms.md",
    };
}
