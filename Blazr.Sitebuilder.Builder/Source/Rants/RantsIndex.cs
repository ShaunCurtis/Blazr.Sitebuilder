using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Rants/index")]

public class RantsIndex : CategoryIndexBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "List of Rants",
        Author = "Shaun Curtis",
        Category = "Rants",
        Description = "List of Rants",
        HideInNavigationLists = true,
    };
}
