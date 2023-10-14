/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

namespace Blazr.SiteBuilder;

public partial class HtmlFactory
{
    public void AddMeta(string name, string content)
        => page.AppendLine($"<meta name=\"{name}\" content=\"{content}\" >");

    public void AddHeaderTitle(string content)
        => page.AppendLine($"<title>{content}</title>");

    public void AddStylesheet(string href)
        => page.AppendLine($"<link href=\"{href}\" rel=\"stylesheet\" type=\"text/css\" >");
}
