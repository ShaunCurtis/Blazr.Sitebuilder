/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

namespace Blazr.SiteBuilder;

public interface IPageData
{
    public string Title { get; }
    public string Description { get; }
    public string Author { get; }
    public string Category { get; }
    public string Story { get; }
    public int Order { get; }
    public string Tags { get; }
    public string MdFile { get; }
    public DateOnly PublishDate { get; }
    public DateOnly LastUpdated { get; }
    public bool HideInNavigationLists { get; }
    public bool DefaultRoute { get; }
}
