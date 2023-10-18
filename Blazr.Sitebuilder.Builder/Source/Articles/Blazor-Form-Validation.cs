using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Articles/Blazor-Form-Validation")]
public class Blazor_Form_Validation : ArticlesFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Blazor Form Validation",
        PublishDate = new DateOnly(2021, 3, 11),
        LastUpdated = new(2021, 3, 16),
        Author = "Shaun Curtis",
        Category = "Articles",
        Tags = "#Blazor;",
        Description = "A Blazor validation control to manage and monitor validation state in a form.",
        MarkdownFile = $@".\Source\Articles\Blazor-Form-Validation.md",
    };
}
