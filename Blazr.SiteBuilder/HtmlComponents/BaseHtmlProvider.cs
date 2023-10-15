/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

namespace Blazr.SiteBuilder;

public static class BaseHtmlProvider
{
    public static string? SeoHeaderHtml(IPageData data)
    {
        var builder = new HtmlFactory();

        builder.AddHeaderTitle(data.Title);
        builder.AddMeta("author", data.Author);
        builder.AddMeta("description", data.Author);


        return builder.GetHtml();
    }

    public static string? OgHeaderHtml(IPageData pageData, ISiteData siteData)
    {
        var builder = new HtmlFactory();

        builder.AddMeta("og:site_name", siteData.SiteName);
        builder.AddMeta("og:site", siteData.SiteUrl);
        builder.AddMeta("og:title", pageData.Title);
        builder.AddMeta("og:description", pageData.Description);

        //<meta property = "og:image" content="" /> <!-- image link, make sure it's jpg -->
        //<meta property = "og:url" content="" /> <!-- where do you want your post to link to -->
        //<meta name = "twitter:card" content="summary_large_image"> <!-- to have large image post format in Twitter -->

        return builder.GetHtml();
    }
}
