using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

public class RantsFolderBase : ArticleBase
{
    public override PageData PageData { get; } = new()
    {
        Author = "Shaun Curtis",
        Category = "Rants",
        Tags = "#Blazor;",
        ContentDirectory = $@".\Source\Rants\",
    };
}
