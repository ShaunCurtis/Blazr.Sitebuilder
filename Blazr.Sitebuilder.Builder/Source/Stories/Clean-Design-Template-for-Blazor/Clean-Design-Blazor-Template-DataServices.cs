using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/stories/Clean-Design-Template-for-Blazor/Clean-Design-Blazor-Template-DataServices")]
public class Clean_Design_Blazor_Template_DataServices : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Data Services",
        PublishDate = new DateOnly(2021, 11, 28),
        LastUpdated = new(2021, 11, 28),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story= "Clean Design Template for Blazor",
        Order=30,
        Tags = "#Blazor;#CleanDesign;",
        Description = "Data Services in the Clean Design Blazor Template.",
        ContentDirectory = $@".\Source\stories\Clean-Design-Template-for-Blazor\",
        MarkdownFile = $@"Clean-Design-Blazor-Template-DataServices.md",
    };
}
