using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Modal-Dialog")]
public class Modal_Dialog : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "A Blazor Modal Dialog",
        PublishDate = new DateOnly(2020, 10, 23),
        LastUpdated = new(2021, 6, 25),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;#Components;",
        Description = "This article describes how to build a modal dialog for Blazor that you can use with any form.",
        MdFile = $@".\Source\Articles\Modal-Dialog.md",
    };
}
