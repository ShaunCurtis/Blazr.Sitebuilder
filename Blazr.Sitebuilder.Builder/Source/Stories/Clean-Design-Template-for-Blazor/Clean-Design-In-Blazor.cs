using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/stories/Clean-Design-Template-for-Blazor/Clean-Design-In-Blazor")]
public class Clean_Design_In_Blazor : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Clean Design Principles in Blazor Applications",
        PublishDate = new DateOnly(2021, 11, 25),
        LastUpdated = new(2021, 11, 25),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "Clean Design Template for Blazor",
        Order = 5,
        Tags = "#Blazor;#CleanDesign;",
        Description = "A series of articles about implementing Clean Design principles in Blazor Applications.",
        ContentDirectory = $@".\Source\stories\Clean-Design-Template-for-Blazor\",
        MarkdownFile = $@"Clean-Design-In-Blazor.md",
    };
}
