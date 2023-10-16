using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/For-ForEach-in-Blazor")]
public class For_ForEach_in_Blazor : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "For and ForEach loops in Blazor Components",
        PublishDate = new DateOnly(2021, 6, 1),
        LastUpdated = new(2021, 11, 26),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;",
        Description = "Using For and ForEach loops in Blazor Components.",
        MdFile = $@".\Source\Building-Blazor-Applications\For-ForEach-in-Blazor.md",
    };
}
