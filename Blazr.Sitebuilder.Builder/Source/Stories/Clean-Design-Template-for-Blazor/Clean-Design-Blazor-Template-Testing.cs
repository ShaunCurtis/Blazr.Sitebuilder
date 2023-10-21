using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Design/Clean-Design-Blazor-Template-Testing")]
public class Clean_Design_Blazor_Template_Testing : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Testing",
        PublishDate = new DateOnly(2021, 11, 25),
        LastUpdated = new(2021, 11, 25),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "Clean Design Template for Blazor",
        Order = 60,
        Tags = "#Blazor;#CleanDesign;",
        Description = "Testing in the Clean Design Blazor Template.",
        ContentDirectory = @".\Source\stories\Clean-Design-Template-for-Blazor\",
        MarkdownFile = "Clean-Design-Blazor-Template-Testing.md",
    };
}
