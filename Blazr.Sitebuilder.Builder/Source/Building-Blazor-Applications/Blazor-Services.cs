using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/Blazor-Services")]
public class Blazor_Services : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Blazor Services",
        PublishDate = new DateOnly(2022, 1, 4),
        LastUpdated = new(2022, 1, 4),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;",
        Description = "An examination of Blazor DI and Services.",
        MdFile = $@".\Source\Building-Blazor-Applications\Blazor-Services.md",
    };
}
