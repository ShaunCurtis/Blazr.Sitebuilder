using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/A-Blazor-Database-Primer/Chapter-1")]
public class A_Blazor_Database_Primer_Chapter1 : StoryObseleteBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Chapter 1 - Project Design and Structure",
        PublishDate = new DateOnly(2021, 8, 13),
        LastUpdated = new(2021, 8, 13),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story= "A Blazor Database Primer",
        Order = 10,
        Tags = "#Blazor;#Database;",
        Description = "This chapter looks at how to structure your application and what design methodologies to apply.",
        ContentDirectory = @".\Source\Stories\A-Blazor-Database-Primer\",
        MarkdownFile = "Chapter-1.md",
    };
}
