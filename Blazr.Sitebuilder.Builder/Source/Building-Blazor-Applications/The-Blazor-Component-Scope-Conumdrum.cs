using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/The-Blazor-Component-Scope-Conumdrum")]
public class The_Blazor_Component_Scope_Conumdrum : BuildingBlazorApplicationsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "The Blazor Component Scope Conundrum",
        PublishDate = new DateOnly(2022, 12, 23),
        LastUpdated = new(2022, 12, 26),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;",
        Description = "Building a Service Manager for Blazor Components.",
        MarkdownFile = $"The-Blazor-Component-Scope-Conumdrum.md",
    };
}
