using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/Notification-Service-Pattern")]
public class Notification_Service_Pattern : BuildingBlazorApplicationsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Blazor Notification Service Pattern",
        PublishDate = new DateOnly(2021, 3, 31),
        LastUpdated = new(2021, 3, 31),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;#CSS",
        Description = "Blazor Notification Service Pattern.",
        MarkdownFile = $"Notification-Service-Pattern.md",
    };
}
