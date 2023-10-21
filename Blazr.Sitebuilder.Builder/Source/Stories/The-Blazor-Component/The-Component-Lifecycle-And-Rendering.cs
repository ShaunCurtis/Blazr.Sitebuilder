using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/The-Blazor-Component/The-Component-Lifecycle-And-Rendering")]
public class The_Component_Lifecycle_And_Rendering : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "The Component Lifecycle And Rendering",
        PublishDate = new DateOnly(2022, 11, 17),
        LastUpdated = new(2022, 11, 17),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "The Blazor Component",
        Order = 40,
        Tags = "#Blazor;#Component;",
        Description = "Exploring the Component lifecycle And rendering process.",
        ContentDirectory = $@".\Source\stories\The-Blazor-Component\",
        MarkdownFile = "The-Component-Lifecycle-And-Rendering.md",
    };
}
