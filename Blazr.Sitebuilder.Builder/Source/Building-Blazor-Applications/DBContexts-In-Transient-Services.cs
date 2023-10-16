using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/DBContexts-In-Transient-Services")]
public class DBContexts_In_Transient_Services : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Notes on DbContexts in Transient Services",
        PublishDate = new DateOnly(2020, 10, 1),
        LastUpdated = new(2020, 10, 1),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#DbContext;",
        Description = "Why You Shouldn't use DbContexts in Transient Services.",
        MdFile = $@".\Source\Building-Blazor-Applications\DBContexts-In-Transient-Services.md",
    };
}
