using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/A-Blazor-Database-Primer/Chapter-6")]
public class A_Blazor_Database_Primer_Chapter6 : StoryObseleteBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Chapter 6 -  Rebuilding FetchData",
        PublishDate = new DateOnly(2021, 8, 13),
        LastUpdated = new(2021, 8, 13),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story= "A Blazor Database Primer",
        Order=60,
        Tags = "#Blazor;#Database;",
        Description = "This chapter focuses on rebuilding the FetchData page.",
        ContentDirectory = $@".\Source\Stories\A-Blazor-Database-Primer\",
        MarkdownFile = "Chapter-6.md",
    };
}
