using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/Building-A-Blazor-AutoComplete-Control")]
public class Building_A_Blazor_AutoComplete_Control : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Building a Blazor AutoComplete Control",
        PublishDate = new DateOnly(2022, 11, 22),
        LastUpdated = new(2022, 12, 19),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;",
        Description = "How to build your own Blazor AutoComplete Control.",
        MdFile = $@".\Source\Building-Blazor-Applications\Building-A-Blazor-AutoComplete-Control.md",
    };
}
