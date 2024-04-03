using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Rants/Sloppy-Code-The-Pattern-Mismatch")]
public class Sloppy_Code_The_Pattern_Mismatch : RantsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "Sloppy Code - The Pattern Mismatch",
        PublishDate = new DateOnly(2024, 02, 15),
        LastUpdated = new(2024, 02, 15),
        Author = "Shaun Curtis",
        Category = "Rants",
        Tags = "#Blazor; #Delegates",
        Description = "Compilers often let you get away with delegate mismatches.",
        MarkdownFile = "Sloppy-Code-The-Pattern-Mismatch.md",
    };
}
