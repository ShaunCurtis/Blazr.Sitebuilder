using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Posts/Multitasking")]
public class Mutlitasking : PostsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Multitasking",
        PublishDate = new DateOnly(2024, 04, 02),
        LastUpdated = new(2024, 04, 02),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#multitasking;",
        Description = "The multitasking Misconception.",
        MarkdownFile = "Multitasking.md",
    };
}
