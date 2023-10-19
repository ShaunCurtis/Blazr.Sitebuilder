using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/stories/Clean-Design-Template-for-Blazor/Index")]
public class Clean_Design_Blazor_Template : ArticleBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "A Blazor Clean Design Template",
        PublishDate = new DateOnly(2021, 11, 25),
        LastUpdated = new(2021, 11, 25),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "Clean Design Template for Blazor",
        Order = 10,
        Tags = "#Blazor;#CleanDesign;",
        Description = "A series of articles about implementing Clean Design in Blazor Applications.",
        ContentDirectory = $@".\Source\stories\Clean-Design-Template-for-Blazor\",
        MarkdownFile = $@"Clean-Design-Blazor-Template.md",
    };
}
