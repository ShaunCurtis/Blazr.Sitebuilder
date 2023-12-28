using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/index")]

public class ArticlesIndex : CategoryIndexBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "List of Articles",
        Author = "Shaun Curtis",
        Category = "Articles",
        Description = "List of Published Articles",
        HideInNavigationLists = true,
    };
}
