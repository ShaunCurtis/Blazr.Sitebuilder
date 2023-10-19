using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/Leaner-Meaner-Greener-Components")]
public class Leaner_Meaner_Greener_Components : BuildingBlazorApplicationsFolderBase
{
    public override PageData PageData => base.PageData with
    { 
        Title = "Leaner, Meaner, Greener Components",
        PublishDate = new DateOnly(2022, 11, 4),
        LastUpdated = new(2022, 11, 4),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;#Components;",
        Description = "Building Leaner, Meaner, Greener Blazor Components - AKA Rethinking the Blazor Component.",
        MarkdownFile = $"Leaner-Meaner-Greener-Components.md",
    };
}
