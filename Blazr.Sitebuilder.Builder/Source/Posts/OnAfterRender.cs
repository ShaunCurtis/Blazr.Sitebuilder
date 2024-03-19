using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Posts/OnAfterRender")]
public class OnAfterRender : PostsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "The Trouble with OnAfterRender",
        PublishDate = new DateOnly(2024, 03, 18),
        LastUpdated = new(2024, 03, 18),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Net; #Blazor;",
        Description = "Why you shouldn't use OnAfterRender to mutate the state of a component.",
        MarkdownFile = "OnAfterRender.md",
    };
}
