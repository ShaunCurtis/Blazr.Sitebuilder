using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/The-Blazor-Component/Introduction")]
public class Component_Introduction : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Introduction",
        PublishDate = new DateOnly(2022, 11, 17),
        LastUpdated = new(2022, 11, 17),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "The Blazor Component",
        Order = 10,
        Tags = "#Blazor;#Component;",
        Description = "Exploring the Out-of-the-Box ComponentBase.",
        ContentDirectory = $@".\Source\stories\The-Blazor-Component\",
        MarkdownFile = "Component-Introduction.md",
    };
}
