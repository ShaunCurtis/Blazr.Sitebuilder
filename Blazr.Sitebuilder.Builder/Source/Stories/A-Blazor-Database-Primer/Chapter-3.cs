using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/A-Blazor-Database-Primer/Chapter-3")]
public class A_Blazor_Database_Primer_Chapter3 : ArticleBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "A Blazor Database Primer - Chapter 3 - The Business and Application Code",
        PublishDate = new DateOnly(2021, 8, 13),
        LastUpdated = new(2021, 8, 13),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story= "A Blazor Database Primer",
        Order=3,
        Tags = "#Blazor;#Components;",
        Description = "The Business and Application Code.",
        ContentDirectory = $@".\Source\Stories\A-Blazor-Database-Primer\",
        MarkdownFile = $@"Chapter-2.md",
    };
}
