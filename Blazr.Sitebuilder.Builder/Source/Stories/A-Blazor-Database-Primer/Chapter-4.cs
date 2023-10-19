using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/A-Blazor-Database-Primer/Chapter-4")]
public class A_Blazor_Database_Primer_Chapter4 : StoryObseleteBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Chapter 4 - Setting up the Solution to Run",
        PublishDate = new DateOnly(2021, 8, 13),
        LastUpdated = new(2021, 8, 13),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story= "A Blazor Database Primer",
        Order=40,
        Tags = "#Blazor;#Database;",
        Description = "This chapter looks at how to structure the solution to run as a Blazor Server SPA.",
        ContentDirectory = $@".\Source\Stories\A-Blazor-Database-Primer\",
        MarkdownFile = "Chapter-4.md",
    };
}
