using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/A-Blazor-Database-Primer/Chapter-2")]
public class A_Blazor_Database_Primer_Chapter2 : ArticleBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "A Blazor Database Primer - Chapter 2 - The Data Store and Data Classes",
        PublishDate = new DateOnly(2021, 8, 13),
        LastUpdated = new(2021, 8, 13),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story= "A Blazor Database Primer",
        Order=2,
        Tags = "#Blazor;#Components;",
        Description = "The Data Store and Data Classes.",
        ContentDirectory = $@".\Source\Stories\A-Blazor-Database-Primer\",
        MarkdownFile = $@"Chapter-2.md",
    };
}
