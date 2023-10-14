/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

namespace Blazr.SiteBuilder;

public static class ArticleHtmlProvider
{
    public static string? BuildArticleDatesHtml(PageData data)
    {
        var builder = new HtmlFactory();

        builder.OpenElement("div", "article-info p-2", $"Published: {data.PublishDate}");
        {
            builder.Add("div", $"Published: {data.PublishDate}");
            builder.Add("div", $"Last Updated: {data.LastUpdated}");
            builder.Add("div", $"Author: {data.Author}");

        }
        builder.CloseElement("div");

        return builder.GetHtml();
    }

    public static string? BuildArticleTitleHtml(PageData data)
    {
        var builder = new HtmlFactory();

        builder.OpenElement("div", "pt-2 pb-2 border-bottom");

        builder.Add("h1", data.Title);
        builder.Add("div", data.Description);

        builder.CloseElement("div");

        return builder.GetHtml();
    }
}
