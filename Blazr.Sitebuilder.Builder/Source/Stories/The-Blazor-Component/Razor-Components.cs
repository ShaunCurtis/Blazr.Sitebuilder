using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/The-Blazor-Component/Razor-Components")]
public class Razor_Components : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Razor Components",
        PublishDate = new DateOnly(2022, 11, 17),
        LastUpdated = new(2022, 11, 17),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "The Blazor Component",
        Order = 50,
        Tags = "#Blazor;#Component;",
        Description = "Exploring Razor Components.",
        ContentDirectory = $@".\Source\stories\The-Blazor-Component\",
        MarkdownFile = "Razor-Components.md",
    };
}
