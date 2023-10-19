using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/A-Blazor-Database-Primer/Index")]

public class A_Blazor_Database_Primer : StoryObseleteBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "A Blazor Database Primer",
        PublishDate = new DateOnly(2021, 8, 13),
        LastUpdated = new(2021, 8, 13),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "A Blazor Database Primer",
        Order = 1,
        Tags = "#Blazor;#Database;",
        Description = "You've built the out-of-the-box template and played around a little with the code.  You're now ready to start your first application.",
        ContentDirectory = @".\Source\Stories\A-Blazor-Database-Primer\",
        MarkdownFile = "A-Blazor-Database-Primer.md",
    };
}
