using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Building-Blazor-List-Components")]
public class Building_Blazor_List_Components : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Building Blazor List Components and the Notification Pattern",
        PublishDate = new DateOnly(2022, 2, 28),
        LastUpdated = new(2022, 2, 28),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;#Components;",
        Description = "This article looks at how to handle large lists in Blazor, decouple the data from the UI, and use the Notification Pattern to trigger updates in components.",
        MdFile = $@".\Source\Articles\Building-Blazor-List-Components.md",
    };
}
