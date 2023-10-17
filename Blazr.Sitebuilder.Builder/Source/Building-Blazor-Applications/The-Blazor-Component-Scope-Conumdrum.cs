using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/The-Blazor-Component-Scope-Conumdrum")]
public class The_Blazor_Component_Scope_Conumdrum : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "The Blazor Component Scope Conundrum",
        PublishDate = new DateOnly(2022, 12, 23),
        LastUpdated = new(2022, 12, 26),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;",
        Description = "Building a Service Manager for Blazor Components.",
        MdFile = $@".\Source\Building-Blazor-Applications\The-Blazor-Component-Scope-Conumdrum.md",
    };
}
