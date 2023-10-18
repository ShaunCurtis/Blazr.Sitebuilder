using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

public class BuildingBlazorApplicationsFolderBase : ArticleBase
{
    public override PageData PageData { get; } = new()
    {
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        ContentDirectory = $@".\Source\Building-Blazor-Applications\",
    };
}
