using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/stories/Clean-Design-Template-for-Blazor/Clean-Design-Blazor-Template-UI")]
public class Clean_Design_Blazor_Template_UI : ArticleBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "The UI in the Clean Design Blazor Template",
        PublishDate = new DateOnly(2021, 11, 25),
        LastUpdated = new(2021, 11, 25),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "Clean Design Template for Blazor",
        Order = 40,
        Tags = "#Blazor;#CleanDesign;",
        Description = "The UI in the Clean Design Blazor Template.",
        ContentDirectory = $@".\Source\stories\Clean-Design-Template-for-Blazor\",
        MarkdownFile = $@"Clean-Design-Blazor-Template-UI.md",
    };
}
