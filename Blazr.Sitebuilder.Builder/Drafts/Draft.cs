using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

//[Route("/Articles/Draft")]
public class Draft : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Draft",
        PublishDate = new DateOnly(2021, 2, 26),
        LastUpdated = new(2021, 2, 26),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "Draft.",
        MarkdownFile = "Draft.md",
    };
}
