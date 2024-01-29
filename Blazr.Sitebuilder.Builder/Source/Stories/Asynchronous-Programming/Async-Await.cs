using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/Asynchronous-Programming/Async-Await")]
public class Async_Await_Base : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Async/Await",
        PublishDate = new DateOnly(2023, 01, 29),
        LastUpdated = new(2023, 01, 29),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "Asynchronous Programming",
        Order = 20,
        Tags = "#async;#await;",
        Description = "What Async/Await really does.",
        ContentDirectory = $@".\Source\stories\Asynchronous-Programming\",
        MarkdownFile = "Async-Await.md",
    };
}
