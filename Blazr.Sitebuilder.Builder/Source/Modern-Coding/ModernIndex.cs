using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Modern-Coding/index")]

public class ModernIndex : CategoryIndexBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "List of Contents",
        Author = "Shaun Curtis",
        Category = "Modern-Coding",
        Description = "List of Published Articles",
        HideInNavigationLists = true,
    };
}
