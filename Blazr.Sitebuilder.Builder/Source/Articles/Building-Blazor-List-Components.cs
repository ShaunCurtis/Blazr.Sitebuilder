using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Building-Blazor-List-Components")]
public class Building_Blazor_List_Components : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Building Blazor List Components and the Notification Pattern",
        PublishDate = new DateOnly(2022, 2, 28),
        LastUpdated = new(2022, 2, 28),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;#Components;",
        Description = "This article looks at how to handle large lists in Blazor, decouple the data from the UI, and use the Notification Pattern to trigger updates in components.",
        MarkdownFile = $@"Building-Blazor-List-Components.md",
    };
}
