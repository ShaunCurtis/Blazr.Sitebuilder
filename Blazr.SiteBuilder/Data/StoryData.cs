/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

namespace Blazr.SiteBuilder;

public record StoryData
{
    public string Story { get; } = string.Empty;

    public StoryData(string story)
    {
        Story = story;
    }
}
