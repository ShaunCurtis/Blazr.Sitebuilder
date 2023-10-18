using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Async-Programming-in-DotNetCore")]
public class Async_Programming_in_DotNetCore : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Async Programming in DotNetCore",
        PublishDate = new DateOnly(2021,2 , 6),
        LastUpdated = new(2021, 2, 6),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Async;#DotNetCore;",
        Description = "A Practical Examination of Async Programming in DotNetCore..",
        MarkdownFile = $"Async-Programming-in-DotNetCore.md",
    };
}
