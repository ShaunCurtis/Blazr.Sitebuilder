using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/The-Blazor-Component/The-Supporting-Cast")]
public class The_Supporting_Cast : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "The Supporting Cast",
        PublishDate = new DateOnly(2022, 11, 17),
        LastUpdated = new(2022, 11, 17),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "The Blazor Component",
        Order = 20,
        Tags = "#Blazor;#Component;",
        Description = "The rendering system supporting classes and other objects.",
        ContentDirectory = $@".\Source\stories\The-Blazor-Component\",
        MarkdownFile = "The-Supporting-Cast.md",
    };
}
