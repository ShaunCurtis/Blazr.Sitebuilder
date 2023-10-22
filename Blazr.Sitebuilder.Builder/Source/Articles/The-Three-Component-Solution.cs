using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/The-Three-Component-Solution")]
public class The_Three_Component_Solution : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Building a Base Component Library - The Three Component Solution",
        PublishDate = new DateOnly(2023, 9, 26),
        LastUpdated = new(2023, 9, 26),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "In this article I'll describe how to build three base components you can use throughout your applications.",
        MarkdownFile = "The-Three-Component-Solution.md",
    };
}
