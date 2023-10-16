﻿using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/XML-XSD-Serialization")]
public class XML_XSD_Serialization : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "DotNetCore XML/XSD Serialization",
        PublishDate = new DateOnly(2021, 2, 26),
        LastUpdated = new(2021, 2, 26),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Serialization;",
        Description = "A quick method to Deserialize Data with a XSD definition",
        MdFile = $@".\Source\Articles\XML-XSD-Serialization.md",
    };
}
