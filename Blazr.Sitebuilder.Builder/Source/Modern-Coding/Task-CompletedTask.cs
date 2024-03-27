using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Modern-Coding/Task-CompletedTask")]
public class Task_CompletedTask : ArticleBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Task.CompletedTask",
        PublishDate = new DateOnly(2024, 03, 27),
        LastUpdated = new(2024, 03, 27),
        Author = "Shaun Curtis",
        Category = "Modern-Coding",
        Tags = "#Task;#CSharp;",
        Description = "Is Task.CompletedTask Expensive?",
        ContentDirectory = @".\Source\Modern-Coding\",
        MarkdownFile = "Task-CompletedTask.md",
    };
}
