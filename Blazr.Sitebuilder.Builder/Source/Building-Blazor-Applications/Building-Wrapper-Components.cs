using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/Building-Wrapper-Components")]
public class Building_Wrapper_Components : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Building a Wrapper Component",
        PublishDate = new DateOnly(2021, 6, 1),
        LastUpdated = new(2021, 11, 26),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;",
        Description = "How to build a wrapper component for Blazor.",
        MdFile = $@".\Source\Building-Blazor-Applications\Building-Wrapper-Components.md",
    };
}
