using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/Building-a-Database-Application-in-Blazor/index")]
public class Building_a_Database_Application_in_Blazor : StoryObseleteBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Building a Database Application in Blazor",
        PublishDate = new DateOnly(2021, 7, 7),
        LastUpdated = new(2021, 7, 7),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story= "Building a Database Application in Blazor",
        Order = 10,
        Tags = "#Blazor;#Database;",
        Description = "A set of Articles about building a Database Application in Blazor.",
        ContentDirectory = @".\Source\Stories\Building-a-Database-Application-in-Blazor\",
        MarkdownFile = "Building-a-Database-Application-in-Blazor.md",
    };
}
