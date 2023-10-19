using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/A-Blazor-Database-Primer/Chapter-3")]
public class A_Blazor_Database_Primer_Chapter3 : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Chapter 3 - The Business and Application Code",
        PublishDate = new DateOnly(2021, 8, 13),
        LastUpdated = new(2021, 8, 13),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story= "A Blazor Database Primer",
        Order=30,
        Tags = "#Blazor;#Components;",
        Description = "This chapter looks at structuring the business logic and application code.",
        ContentDirectory = $@".\Source\Stories\A-Blazor-Database-Primer\",
        MarkdownFile = "Chapter-3.md",
    };
}
