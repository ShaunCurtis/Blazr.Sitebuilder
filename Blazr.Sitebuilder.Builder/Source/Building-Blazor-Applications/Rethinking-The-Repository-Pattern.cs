using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/Rethinking-The-Repository-Pattern")]
public class Rethinking_The_Repository_Pattern : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Rethinking The Repository Pattern",
        PublishDate = new DateOnly(2022, 12, 7),
        LastUpdated = new(2022, 12, 7),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;#Repository;",
        Description = "An article taking a fresh look at how to implement the Repository Pattern.",
        MdFile = $@".\Source\Building-Blazor-Applications\Rethinking-The-Repository-Pattern.md",
    };
}
