using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Modern-Coding/Nullable-And-VS-2022")]
public class Nullable_And_VS_2022 : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Nullable in Visual Studio 2022",
        PublishDate = new DateOnly(2021, 11, 27),
        LastUpdated = new(2021, 11, 27),
        Author = "Shaun Curtis",
        Category = "Modern-Coding",
        Tags = "#Nullable;#CSharp;",
        Description = "Notes about Nullable and breaking changes in Visual Studio 2022.",
        MdFile = $@".\Source\Modern-Coding\Nullable-And-VS-2022.md",
    };
}
