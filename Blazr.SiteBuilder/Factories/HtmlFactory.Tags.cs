/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

namespace Blazr.SiteBuilder;

public partial class HtmlFactory
{
    public void Add(string tag, string cssClass, string content)
        => page.AppendLine($"<{tag} class=\"{cssClass}\">{content}</{tag}>");

    public void Add(string tag, string content)
        => page.AppendLine($"<{tag}>{content}</{tag}>");

    public void Add(string content)
        => page.AppendLine(content);

    public void Add(HtmlFragment fragment)
    {
        var builder = new HtmlFactory();
        fragment.Invoke(builder);
        page.Append(builder);
    }

    public void AddClosedTag(string tag, string cssClass)
        => page.AppendLine($"<{tag} class=\"{cssClass}\"/>");

    public void AddDiv(string cssClass, string content)
        => page.AppendLine($"<div class=\"{cssClass}\">{content}</div>");

    public void AddAnchor(string cssClass, string href, string content)
        => page.AppendLine($"<a class=\"{cssClass}\" href=\"{href}\">{content}</a>");

}
