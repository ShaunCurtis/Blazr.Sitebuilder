using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/Building-a-Database-Application-in-Blazor/Building-a-Database-Application-in-Blazor-Part-1")]
public class Building_a_Database_Application_in_Blazor_Part_1 : StoryObseleteBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Project Structure and Framework",
        PublishDate = new DateOnly(2021, 7, 7),
        LastUpdated = new(2021, 7, 7),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story= "Building a Database Application in Blazor",
        Order = 10,
        Tags = "#Blazor;#Database;",
        Description = "This article describes the Structure and Framework for Blazor Database Template.",
        ContentDirectory = @".\Source\Stories\Building-a-Database-Application-in-Blazor\",
        MarkdownFile = "Building-a-Database-Application-in-Blazor-Part-1.md",
    };
}
