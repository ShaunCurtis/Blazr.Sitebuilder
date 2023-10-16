using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/A-Blazor-Database-Primer/Chapter-1")]
public class A_Blazor_Database_Primer_Chapter1 : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "A Blazor Database Primer - Chapter 1 - Project Design and Structure",
        PublishDate = new DateOnly(2021, 8, 13),
        LastUpdated = new(2021, 8, 13),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story= "A Blazor Database Primer",
        Order = 1,
        Tags = "#Blazor;#Components;",
        Description = "This article chapter looks at how to structure your application and what design methodologies to apply.",
        MdFile = $@".\Source\Stories\A-Blazor-Database-Primer\Chapter-1.md",
    };
}
