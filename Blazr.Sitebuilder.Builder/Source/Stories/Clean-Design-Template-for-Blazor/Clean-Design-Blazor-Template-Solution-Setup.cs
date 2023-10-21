using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Design/Clean-Design-Blazor-Template-Solution-Setup")]
public class Clean_Design_Blazor_Template_Solution_Setup : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Solution Setup",
        PublishDate = new DateOnly(2021, 11, 25),
        LastUpdated = new(2021, 11, 25),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "Clean Design Template for Blazor",
        Order = 50,
        Tags = "#Blazor;#CleanDesign;",
        Description = "Solution Setup in the Clean Design Blazor Template.",
        ContentDirectory = $@".\Source\stories\Clean-Design-Template-for-Blazor\",
        MarkdownFile = $@"Clean-Design-Blazor-Template-Solution-Setup.md",
    };
}
