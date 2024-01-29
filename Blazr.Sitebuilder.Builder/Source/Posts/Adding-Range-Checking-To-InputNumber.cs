using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Posts/Adding-Range-Checking-To-InputNumber")]
public class Adding_Range_Checking_To_InputNumber : PostsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Adding Range Checking To InputNumber",
        PublishDate = new DateOnly(2023, 11, 22),
        LastUpdated = new(2023, 11, 22),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;",
        Description = "This post shows how to modify the InputNumber component to integrate range checking.",
        MarkdownFile = "Adding-Range-Checking-To-InputNumber.md",
    };
}
