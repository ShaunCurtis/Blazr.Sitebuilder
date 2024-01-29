using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/The-Blazor-Component-Registration-Pattern")]
public class The_Blazor_Component_Registration_Pattern : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "The Blazor Component Registration Pattern",
        PublishDate = new DateOnly(2023, 11, 21),
        LastUpdated = new(2023, 11, 21),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "This article demonstrates the basics of the Blazor Component Registration pattern.",
        MarkdownFile = "The-Blazor-Component-Registration-Pattern.md",
    };
}
