using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/A-Blazor-Database-Primer/Chapter-5")]
public class A_Blazor_Database_Primer_Chapter5 : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Chapter 5 - Adding a WASM SPA to the Solution",
        PublishDate = new DateOnly(2021, 8, 13),
        LastUpdated = new(2021, 8, 13),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story= "A Blazor Database Primer",
        Order=50,
        Tags = "#Blazor;#Database;",
        Description = "This chapter looks at how to structure the solution to run as an WASM SPA.",
        ContentDirectory = $@".\Source\Stories\A-Blazor-Database-Primer\",
        MarkdownFile = "Chapter-5.md",
    };
}
