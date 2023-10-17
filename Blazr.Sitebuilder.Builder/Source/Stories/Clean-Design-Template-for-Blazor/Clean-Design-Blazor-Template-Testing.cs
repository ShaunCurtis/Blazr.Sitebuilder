using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/stories/Clean-Design-Template-for-Blazor/Clean-Design-Blazor-Template-Testing")]
public class Clean_Design_Blazor_Template_Testing : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "A Blazor Clean Design Template",
        PublishDate = new DateOnly(2021, 11, 25),
        LastUpdated = new(2021, 11, 25),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "Clean Design Template for Blazor",
        Order = 60,
        Tags = "#Blazor;#CleanDesign;",
        Description = "A Clean Design Template for Blazor.",
        MdFile = $@".\Source\stories\Clean-Design-Template-for-Blazor\Clean-Design-Blazor-Template-Testing.md",
    };
}
