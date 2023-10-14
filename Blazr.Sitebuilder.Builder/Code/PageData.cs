namespace Blazr.Sitebuilder.Builder.Code;

public record PageData
{
    public string Title { get; init; } = "Not Set";
    public string Description { get; init; } = "Not Set";
    public string Author { get; init; } = "Not Set";
    public string MdFile { get; init; } = "article.md";
    public DateOnly PublishDate { get; init; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly LastUpdated { get; init; } = DateOnly.FromDateTime(DateTime.Now);
}

