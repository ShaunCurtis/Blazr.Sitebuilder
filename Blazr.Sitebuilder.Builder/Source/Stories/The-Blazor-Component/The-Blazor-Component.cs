using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/The-Blazor-Component/Index")]
public class The_Blazor_Component_Index : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "An In-Depth look at the Blazor Component",
        PublishDate = new DateOnly(2022, 11, 17),
        LastUpdated = new(2022, 11, 17),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "The Blazor Component",
        Order = 1,
        Tags = "#Blazor;#Component;",
        Description = "A series of short articles taking an in-depth look at the Blazor Component.",
        ContentDirectory = $@".\Source\stories\The-Blazor-Component\",
        MarkdownFile = "The-Blazor-Component.md",
        HideInNavigationLists = true
    };
}
