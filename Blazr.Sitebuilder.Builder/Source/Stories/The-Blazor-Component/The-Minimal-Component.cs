using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/The-Blazor-Component/The-Minimal-Component")]
public class The_Minimal_Component : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "The Minimal Component",
        PublishDate = new DateOnly(2022, 11, 17),
        LastUpdated = new(2022, 11, 17),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "The Blazor Component",
        Order = 30,
        Tags = "#Blazor;#Component;",
        Description = "In the introduction we saw a very minimal component.  In this chapter we transform that base into a fully functional base component.",
        ContentDirectory = $@".\Source\stories\The-Blazor-Component\",
        MarkdownFile = "The-Minimal-Component.md",
    };
}
