using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/The-Blazor-Component/The-Renderer-And-Events")]
public class The_Renderer_And_Events : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "The Renderer and Events",
        PublishDate = new DateOnly(2022, 11, 17),
        LastUpdated = new(2022, 11, 17),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "The Blazor Component",
        Order = 70,
        Tags = "#Blazor;#Component;",
        Description = "Exploring the Render process and events.",
        ContentDirectory = $@".\Source\stories\The-Blazor-Component\",
        MarkdownFile = "The-Renderer-And-Events.md",
    };
}
