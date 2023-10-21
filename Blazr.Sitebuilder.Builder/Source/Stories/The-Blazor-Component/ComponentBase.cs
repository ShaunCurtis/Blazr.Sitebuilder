using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/The-Blazor-Component/ComponentBase")]
public class Component_Base : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "ComponentBase",
        PublishDate = new DateOnly(2022, 11, 17),
        LastUpdated = new(2022, 11, 17),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "The Blazor Component",
        Order = 60,
        Tags = "#Blazor;#Component;",
        Description = "Exploring the Out-of-the-Box ComponentBase.",
        ContentDirectory = $@".\Source\stories\The-Blazor-Component\",
        MarkdownFile = "ComponentBase.md",
    };
}
