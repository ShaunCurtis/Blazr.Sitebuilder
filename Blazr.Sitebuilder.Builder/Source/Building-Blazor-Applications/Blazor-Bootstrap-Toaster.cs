using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/Blazor-Bootstrap-Toaster")]
public class Blazor_Bootstrap_Toaster : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Blazor Bootstrap Toaster",
        PublishDate = new DateOnly(2022, 1, 1),
        LastUpdated = new(2022, 1, 1),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;",
        Description = "Building a Blazor Bootstrap Toaster.",
        MdFile = $@".\Source\Building-Blazor-Applications\Blazor-Bootstrap-Toaster.md",
    };
}
