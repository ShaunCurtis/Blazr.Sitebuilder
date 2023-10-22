using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Rants/The-Program-Code-Wall")]
public class The_Program_Code_Wall : RantsFolderBase
{
    public override PageData PageData => base.PageData with
    {
        Title = "The Program Code Wall",
        PublishDate = new DateOnly(2023, 10, 21),
        LastUpdated = new(2023, 10, 21),
        Author = "Shaun Curtis",
        Category = "Rants",
        Tags = "#Blazor;",
        Description = "How many times have you looked at Program.cs and been totally confused.  The detail initially overwhelms you.",
        MarkdownFile = "The-Program-Code-Wall.md",
    };
}
