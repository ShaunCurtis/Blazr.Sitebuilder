using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/A-Blazor-Database-Primer/Index")]

public class A_Blazor_Database_Primer : StoryIndexBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "A Blazor Database Primer",
        Author = "Shaun Curtis",
        Category = "Stories",
        Story = "A Blazor Database Primer",
        Description = "List of Articles in A Blazor Database Primer",
        HideInNavigationLists = true,
    };
}
