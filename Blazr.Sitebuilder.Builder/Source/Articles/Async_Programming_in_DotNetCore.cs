using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Async-Programming-in-DotNetCore")]

public class Async_Programming_in_DotNetCore : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Async Programming in DotNetCore",
        PublishDate = new DateOnly(2021,2 , 6),
        LastUpdated = new(2021, 2, 6),
        Author = "Shaun Curtis",
        Group = "Articles",
        Tags = "#Async;#DotNetCore;",
        Description = "A Practical Examination of Async Programming in DotNetCore..",
        MdFile = $@".\Source\Articles\Async-Programming-in-DotNetCore.md",
        OutputHtmlFile = $@"\Articles\Async-Programming-in-DotNetCore.md"

    };
}
