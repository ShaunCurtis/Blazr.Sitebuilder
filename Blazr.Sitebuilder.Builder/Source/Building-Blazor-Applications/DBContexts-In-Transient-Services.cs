using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/DBContexts-In-Transient-Services")]
public class DBContexts_In_Transient_Services : BuildingBlazorApplicationsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Notes on DbContexts in Transient Services",
        PublishDate = new DateOnly(2020, 10, 1),
        LastUpdated = new(2020, 10, 1),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#DbContext;#Blazor;",
        Description = "Why You Shouldn't use DbContexts in Transient Services.",
        MarkdownFile = $@"DBContexts-In-Transient-Services.md",
    };
}
