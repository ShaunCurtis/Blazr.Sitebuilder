using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Posts/index")]

public class PostsIndex : CategoryIndexBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "List of Posts",
        Author = "Shaun Curtis",
        Category = "Posts",
        Description = "List of Posts",
        HideInNavigationLists = true,
    };
}
