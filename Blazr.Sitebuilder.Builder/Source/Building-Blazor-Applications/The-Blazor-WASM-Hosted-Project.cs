using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Building-Blazor-Applications/The-Blazor-WASM-Hosted-Project")]
public class The_Blazor_WASM_Hosted_Project : BuildingBlazorApplicationsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "What is the Blazor WASM Hosted Project",
        PublishDate = new DateOnly(2021, 12, 20),
        LastUpdated = new(2021, 12, 20),
        Author = "Shaun Curtis",
        Category = "Posts",
        Tags = "#Blazor;#CSS",
        Description = "A short tour of the Blazor WASM hosted Project.",
        MarkdownFile = "The-Blazor-WASM-Hosted-Project.md",
    };
}
