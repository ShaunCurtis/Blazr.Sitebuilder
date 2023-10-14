﻿/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

namespace Blazr.SiteBuilder;

public record PageData
{
    public string Title { get; init; } = "Not Set";
    public string Description { get; init; } = "Not Set";
    public string Author { get; init; } = "Not Set";
    public string Group { get; init; } = "Articles";
    public string Tags { get; init; } = "";
    public string MdFile { get; init; } = "article.md";
    public string OutputHtmlFile { get; init; } = "article.md";
    public DateOnly PublishDate { get; init; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly LastUpdated { get; init; } = DateOnly.FromDateTime(DateTime.Now);
}
