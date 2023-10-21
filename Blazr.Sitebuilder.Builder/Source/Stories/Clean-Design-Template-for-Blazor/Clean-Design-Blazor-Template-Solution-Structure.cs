using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Design/Clean-Design-Blazor-Template-Solution-Structure")]
public class Clean_Design_Blazor_Template_Solution_Structure : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Solution Structure",
        PublishDate = new DateOnly(2021, 11, 25),
        LastUpdated = new(2021, 11, 25),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "Clean Design Template for Blazor",
        Order = 20,
        Tags = "#Blazor;#CleanDesign;",
        Description = "Solution Structure in the Clean Design Blazor Template.",
        ContentDirectory = $@".\Source\stories\Clean-Design-Template-for-Blazor\",
        MarkdownFile = $@"Clean-Design-Blazor-Template-Solution-Structure.md",
    };
}
