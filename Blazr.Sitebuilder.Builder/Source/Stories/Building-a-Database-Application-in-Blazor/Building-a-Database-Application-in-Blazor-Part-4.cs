using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/Building-a-Database-Application-in-Blazor/Building-a-Database-Application-in-Blazor-Part-4")]
public class Building_a_Database_Application_in_Blazor_Part_4 : StoryObseleteBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Building thw UI Components",
        PublishDate = new DateOnly(2021, 7, 7),
        LastUpdated = new(2021, 7, 7),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story= "Building a Database Application in Blazor",
        Order = 40,
        Tags = "#Blazor;#Database;",
        Description = "This article looks at the components we use in the UI and then focuses on how to build generic UI Components from HTML and CSS.",
        ContentDirectory = @".\Source\Stories\Building-a-Database-Application-in-Blazor\",
        MarkdownFile = "Building-a-Database-Application-in-Blazor-Part-4.md",
    };
}
