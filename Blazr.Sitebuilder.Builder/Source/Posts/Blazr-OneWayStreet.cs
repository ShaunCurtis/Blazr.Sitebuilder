using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Posts/Blazr-OneWayStreet")]
public class Blazr_OneWayStreet : PostsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "One Way Street",
        PublishDate = new DateOnly(2023, 10, 22),
        LastUpdated = new(2023, 10, 22),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;#Database",
        Description = "One Way Street is a read only data pipeline loosely based on CQS [Command/Query Separation] pattern.  This article provides an introduction and demonstrates it's usage using XUnit tests.",
        MarkdownFile = "Blazr-OneWayStreet.md",
    };
}
