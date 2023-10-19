using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Inline-Dialog")]
public class Inline_Dialog : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "A Blazor Inline Dialog Control",
        PublishDate = new DateOnly(2021, 3, 11),
        LastUpdated = new(2021, 3, 17),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "A Blazor inline dialog control to lock all page controls except those within the form.",
        MarkdownFile = "Inline-Dialog.md",
    };
}
