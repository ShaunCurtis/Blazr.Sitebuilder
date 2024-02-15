using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Posts/The_Blazor_Button_Click")]
public class The_Blazor_Button_Click : PostsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "The Blazor Button Click",
        PublishDate = new DateOnly(2024, 02, 14),
        LastUpdated = new(2024, 02, 14),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor; #Net8;",
        Description = "This post explains what goes on behind a Blazor UI event such as a button click.",
        MarkdownFile = "The-Blazor-Button-Click.md",
    };
}
