using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/Notification-Service-Pattern")]
public class Notification_Service_Pattern : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Blazor Notification Service Pattern",
        PublishDate = new DateOnly(2021, 3, 31),
        LastUpdated = new(2021, 3, 31),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;#CSS",
        Description = "Blazor Notification Service Pattern.",
        MdFile = $@".\Source\Building-Blazor-Applications\Notification-Service-Pattern.md",
    };
}
