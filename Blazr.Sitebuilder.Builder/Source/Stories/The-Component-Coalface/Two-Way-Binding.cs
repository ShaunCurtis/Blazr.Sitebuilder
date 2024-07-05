using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/The-Component-Coalface/Two-Way-Binding")]
public class Two_Way_Binding_Base : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Two Way Binding",
        PublishDate = new DateOnly(2024, 07, 02),
        LastUpdated = new(2024, 07, 02),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "The Component Coalface",
        Order = 60,
        Tags = "#Blazor;#Component;",
        Description = "Exploring Two Way Binding in Components.",
        ContentDirectory = $@".\Source\stories\The-Component-Coalface\",
        MarkdownFile = "Two-Way-Binding.md",
    };
}
