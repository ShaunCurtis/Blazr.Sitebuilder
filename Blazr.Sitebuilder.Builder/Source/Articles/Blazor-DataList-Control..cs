using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Blazor-DataList-Control")]
public class Blazor_DataList_Control : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "A Blazor DataList Control",
        PublishDate = new DateOnly(2020, 10, 1),
        LastUpdated = new(2020, 10, 1),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;#Components;",
        Description = "This article describes how to build an input control based on a DataList in Blazor, and make it behave like a Select.  Along the way it looks at how `InputBase` controls are built and work.",
        MarkdownFile = $@"Blazor-DataList-Control.md",
    };
}
