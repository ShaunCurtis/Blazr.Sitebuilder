/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

namespace Blazr.SiteBuilder
{
    public partial class HtmlFactory
    {
        public void AddElement(string tag, string tagContent, HtmlFragment fragment)
        {
            var builder = new HtmlFactory();
            page.AppendLine($"<{tag} {tagContent} >");
            fragment!.Invoke(builder);
            page.Append(builder.GetHtml());
            page.AppendLine($"</{tag}>");
        }

        public void OpenElement(string tag)
            => page.AppendLine($"<{tag}>");

        public void OpenElement(string tag, string cssClass)
            => page.AppendLine($"<{tag} class=\"{cssClass}\" >");

        public void OpenElement(string tag, string cssClass, string tagContent)
            => page.AppendLine($"<{tag} class=\"{cssClass}\" {tagContent} >");

        public void OpenElementWithTagContent(string tag, string tagContent)
            => page.AppendLine($"<{tag} {tagContent} >");

        public void CloseElement(string tag)
            => page.AppendLine($"</{tag}>");

        public void AddDivElement(string tagContent, HtmlFragment fragment)
        {
            var builder = new HtmlFactory();
            page.AppendLine($"<div {tagContent} >");
            fragment!.Invoke(builder);
            page.Append(builder.GetHtml());
            page.AppendLine($"</div>");
        }

        public void AddDivElement(string tagContent)
            => page.AppendLine($"<div {tagContent} >");

        public void CloseDivElement()
            => page.AppendLine($"</div>");

    }
}
