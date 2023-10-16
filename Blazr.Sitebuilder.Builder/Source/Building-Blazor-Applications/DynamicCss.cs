using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/DynamicCss")]
public class DynamicCss : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Blazor Dynamic Stylesheets",
        PublishDate = new DateOnly(2021, 6, 1),
        LastUpdated = new(2021, 11, 26),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;#CSS",
        Description = "How to change Stylesheets out at run time.",
        MdFile = $@".\Source\Building-Blazor-Applications\DynamicCss.md",
    };
}
