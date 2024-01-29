using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/Asynchronous-Programming/index")]
public class Asynchronous_Programming_Index : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "An In-Depth look at Asynchronous Programming",
        PublishDate = new DateOnly(2023, 01, 29),
        LastUpdated = new(2023, 01, 29),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "Asynchronous Programming",
        Order = 1,
        Tags = "#async;",
        Description = "A series of short articles taking an in-depth look at asynchronous programming.",
        ContentDirectory = $@".\Source\stories\Asynchronous-Programming\",
        MarkdownFile = "Asynchronous-Programming.md",
        HideInNavigationLists = true
    };
}
