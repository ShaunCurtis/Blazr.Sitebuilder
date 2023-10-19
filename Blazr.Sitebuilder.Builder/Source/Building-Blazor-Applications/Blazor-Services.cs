using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/Blazor-Services")]
public class Blazor_Services : BuildingBlazorApplicationsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Blazor Services",
        PublishDate = new DateOnly(2022, 1, 4),
        LastUpdated = new(2022, 1, 4),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;",
        Description = "An examination of Blazor DI and Services.",
        MarkdownFile = $@"Blazor-Services.md",
        HideInNavigationLists = false,
    };
}
