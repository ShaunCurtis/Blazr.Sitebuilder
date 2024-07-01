using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/The-Blazor-Component-Tales/Index")]
public class Blazor_Component_Tales_Index : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "The-Blazor-Component-Tales",
        PublishDate = new DateOnly(2024, 06, 30),
        LastUpdated = new(2024, 06, 30),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "The-Blazor-Component-Tales",
        Order = 1,
        Tags = "#Blazor;#Component;",
        Description = "Short articles on Blazor Component Development.",
        ContentDirectory = $@".\Source\stories\The-Blazor-Component-Tales\",
        MarkdownFile = "The-Blazor-Component-Tales.md",
        HideInNavigationLists = true
    };
}
