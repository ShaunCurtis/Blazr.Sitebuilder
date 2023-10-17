/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

public class SiteData : ISiteData
{
    public string SiteName { get; init; } = string.Empty;

    public string SiteUrl { get; init; } = string.Empty;

    private SiteData() { }

    private static SiteData? _siteData;

    public static SiteData GetInstance()
        => _siteData ?? (_siteData = new()
        {
            SiteName = "Cold Elm Coders",
            SiteUrl = "https://shauncurtis.github.io/"
        });
}

public class ThisSiteData : ISiteData
{
    public string SiteName { get; init; } = "Cold Elm Coders";

    public string SiteUrl { get; init; } = @"https://shauncurtis.github.io/";

}
