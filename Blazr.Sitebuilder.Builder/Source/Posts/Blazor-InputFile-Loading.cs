using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Posts/Blazor-InputFile-Loading")]
public class Blazor_InputFile_Loading : PostsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Blazor InputFile Processing",
        PublishDate = new DateOnly(2023, 10, 31),
        LastUpdated = new(2023, 10, 31),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;",
        Description = "This article demonstrates ways to handle processing files selected in an InputFile control including progress and cancellation.",
        MarkdownFile = "Blazor-InputFile-Loading.md",
    };
}
