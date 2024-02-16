using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Posts/Timers")]
public class Timers : PostsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "The DotNetCore Timers",
        PublishDate = new DateOnly(2024, 02, 16),
        LastUpdated = new(2024, 02, 16),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Net; #Timers;",
        Description = "What is a Timer?",
        MarkdownFile = "Timers.md",
    };
}
