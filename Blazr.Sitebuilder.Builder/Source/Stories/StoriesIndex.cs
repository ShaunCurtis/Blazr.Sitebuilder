﻿using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/Index")]

public class StoriesIndex : CategoryIndexBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "List of Stories",
        Author = "Shaun Curtis",
        Category = "Stories",
        Description = "List of Stories",
        HideInNavigationLists = true,
    };
}
