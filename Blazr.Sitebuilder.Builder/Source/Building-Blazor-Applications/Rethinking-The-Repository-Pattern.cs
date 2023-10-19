using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/Rethinking-The-Repository-Pattern")]
public class Rethinking_The_Repository_Pattern : BuildingBlazorApplicationsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Rethinking The Repository Pattern",
        PublishDate = new DateOnly(2022, 12, 7),
        LastUpdated = new(2022, 12, 7),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;#Repository;",
        Description = "An article taking a fresh look at how to implement the Repository Pattern.",
        MarkdownFile = $"Rethinking-The-Repository-Pattern.md",
    };
}
