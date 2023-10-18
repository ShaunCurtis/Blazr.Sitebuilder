using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Blazor-Brick-By-Brick")]
public class Blazor_Brick_By_Brick : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Building a Blazor App from Scratch",
        PublishDate = new DateOnly(2022, 1, 4),
        LastUpdated = new(2022, 1, 4),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "This article describes how to build a Blazor App from the standard AspNetCore Web template.",
        MarkdownFile = $@"Blazor-Brick-By-Brick.md",
    };
}
