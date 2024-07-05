using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/The-Component-Coalface/Index")]
public class Blazor_Component_Tales_Index : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "The Component Coalface",
        PublishDate = new DateOnly(2024, 06, 30),
        LastUpdated = new(2024, 06, 30),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "The Component Coalface",
        Order = 1,
        Tags = "#Blazor;#Component;",
        Description = "Short articles on Blazor Component Development.",
        ContentDirectory = $@".\Source\stories\The-Component-Coalface\",
        MarkdownFile = "The-Component-Coalface.md",
        HideInNavigationLists = true
    };
}
