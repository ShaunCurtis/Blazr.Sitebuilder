using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/stories/Clean-Design-Template-for-Blazor/Clean-Design-Blazor-Template-Solution-Structure")]
public class Clean_Design_Blazor_Template_Solution_Structure : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Solution Structure in the Clean Design Blazor Template",
        PublishDate = new DateOnly(2021, 11, 25),
        LastUpdated = new(2021, 11, 25),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "Clean Design Template for Blazor",
        Order = 20,
        Tags = "#Blazor;#CleanDesign;",
        Description = "Solution Structure in the Clean Design Blazor Template.",
        MdFile = $@".\Source\stories\Clean-Design-Template-for-Blazor\Clean-Design-Blazor-Template-Solution-Structure.md",
    };
}
