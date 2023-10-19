using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/For-ForEach-in-Blazor")]
public class For_ForEach_in_Blazor : BuildingBlazorApplicationsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "For and ForEach loops in Blazor Components",
        PublishDate = new DateOnly(2021, 6, 1),
        LastUpdated = new(2021, 11, 26),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;",
        Description = "Using For and ForEach loops in Blazor Components.",
        MarkdownFile = $@"For-ForEach-in-Blazor.md",
    };
}
