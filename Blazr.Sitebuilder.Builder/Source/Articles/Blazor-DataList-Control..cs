using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Blazor-DataList-Control")]
public class Blazor_DataList_Control : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "A Blazor DataList Control",
        PublishDate = new DateOnly(2020, 10, 1),
        LastUpdated = new(2020, 10, 1),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;#Components;",
        Description = "This article describes how to build an input control based on a DataList in Blazor, and make it behave like a Select.  Along the way it looks at how `InputBase` controls are built and work.",
        MdFile = $@".\Source\Articles\Blazor-DataList-Control.md",
    };
}
