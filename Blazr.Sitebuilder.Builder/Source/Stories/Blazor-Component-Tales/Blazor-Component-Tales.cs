using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/Blazor-Component-Tales/Index")]
public class Blazor_Component_Tales_Index : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Tales from the Component Component Coalface",
        PublishDate = new DateOnly(2024, 06, 30),
        LastUpdated = new(2024, 06, 30),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "Tales from the Component Componenbnt Coalface",
        Order = 1,
        Tags = "#Blazor;#Component;",
        Description = "Short articles on Blazor Component Development.",
        ContentDirectory = $@".\Source\stories\Blazor-Component-Tales\",
        MarkdownFile = "Blazor-Component-Tales.md",
        HideInNavigationLists = true
    };
}
