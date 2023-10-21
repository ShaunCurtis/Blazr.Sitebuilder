using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/The-Blazor-Component/Important-Less-Doumented-Information-And-Final-Observations")]
public class Important_Less_Documented_Information_And_Final_Observations : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Important Less Documented Information And Final Observations",
        PublishDate = new DateOnly(2022, 11, 17),
        LastUpdated = new(2022, 11, 17),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "The Blazor Component",
        Order = 80,
        Tags = "#Blazor;#Component;",
        Description = "Some Final Observations.",
        ContentDirectory = $@".\Source\stories\The-Blazor-Component\",
        MarkdownFile = "Important-Less-Documented-Information-And-Final-Observations.md",
    };
}
