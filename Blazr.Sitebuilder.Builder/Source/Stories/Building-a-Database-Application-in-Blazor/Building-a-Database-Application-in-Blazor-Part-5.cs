using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-a-Database-Application-in-Blazor/Building-a-Database-Application-in-Blazor-Part-5")]
public class Building_a_Database_Application_in_Blazor_Part_5 : StoryObseleteBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "View Components - CRUD List Operations in the UI",
        PublishDate = new DateOnly(2021, 7, 7),
        LastUpdated = new(2021, 7, 7),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story= "Building a Database Application in Blazor",
        Order = 50,
        Tags = "#Blazor;#Database;",
        Description = "This article looks in detail at building reusable List Presentation Layer components and deploying them in both Server and WASM projects.",
        ContentDirectory = @".\Source\Stories\Building-a-Database-Application-in-Blazor\",
        MarkdownFile = "Building-a-Database-Application-in-Blazor-Part-5.md",
    };
}
