using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/Whats-Wrong-with-my-Component-Design")]
public class Whats_Wrong_with_my_Component_Design : BuildingBlazorApplicationsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "What's Wrong with my Component Design?",
        PublishDate = new DateOnly(2022, 11, 7),
        LastUpdated = new(2022, 11, 7),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;",
        Description = "Applying Separation of Concerns, SOLID and Patterns to your Blazor Components.",
        MarkdownFile = "Whats-Wrong-with-my-Component-Design.md",
    };
}
