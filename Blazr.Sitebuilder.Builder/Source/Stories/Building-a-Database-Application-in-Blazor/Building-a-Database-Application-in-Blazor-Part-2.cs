using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/Building-a-Database-Application-in-Blazor/Building-a-Database-Application-in-Blazor-Part-2")]
public class Building_a_Database_Application_in_Blazor_Part_2 : StoryObseleteBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Building the Services",
        PublishDate = new DateOnly(2021, 7, 7),
        LastUpdated = new(2021, 7, 7),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story= "Building a Database Application in Blazor",
        Order = 20,
        Tags = "#Blazor;#Database;",
        Description = "This article describes building the CRUDL data services for a Blazor Database Application.",
        ContentDirectory = @".\Source\Stories\Building-a-Database-Application-in-Blazor\",
        MarkdownFile = "Building-a-Database-Application-in-Blazor-Part-2.md",
    };
}
