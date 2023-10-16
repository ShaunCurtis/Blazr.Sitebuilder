using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Blazor-Components")]
public class Blazor_Async_Programming : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Async Programming in Blazor",
        PublishDate = new DateOnly(2020, 11, 11),
        LastUpdated = new(2021, 4, 20),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "A guide to async programming in Blazor.",
        MdFile = $@".\Source\Articles\Blazor-Async-Programming.md",
    };
}
