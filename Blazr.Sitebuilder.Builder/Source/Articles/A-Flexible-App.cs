﻿using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/A-Flexible-App")]
public class A_Flexible_App : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with 
    {
        Title = "Creating a Dynamic Blazor App Component",
        PublishDate = new DateOnly(2021, 4, 9),
        LastUpdated = new(2021, 4, 13),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "This article shows how to add Dynamic Routing, Layouts and RouteViews to the Blazor App Component.",
        MarkdownFile = $"A-Flexible-App.md",
    };
}
