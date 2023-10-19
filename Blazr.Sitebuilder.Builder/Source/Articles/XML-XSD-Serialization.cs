using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/XML-XSD-Serialization")]
public class XML_XSD_Serialization : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "DotNetCore XML/XSD Serialization",
        PublishDate = new DateOnly(2021, 2, 26),
        LastUpdated = new(2021, 2, 26),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Serialization;",
        Description = "A quick method to Deserialize Data with a XSD definition",
        MarkdownFile = "XML-XSD-Serialization.md",
    };
}
