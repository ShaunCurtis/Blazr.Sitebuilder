/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
namespace Blazr.SiteBuilder;

public static class ArticleTOC
{
    public static MarkupString GetTOC(PageTOC toc)
    {
        var builder = new HtmlFactory();

        builder.Add("h4", "p-2", "Table of Contents");
        builder.OpenElement("ul", "TOC");
        {
            Console.WriteLine($" => Render {toc!.Title} - {toc.Level}");
            GetNode(builder, toc);
        }
        builder.CloseElement("ul");

        return new MarkupString(builder.ToString() ?? string.Empty);
    }

    private static void GetNode(HtmlFactory builder, PageTOC? parentNode)
    {
        builder.OpenElement("li", $"TOC-item TOC-item-{parentNode!.Level}");
        {
            if (!parentNode.Hidden)
                builder.AddAnchor("TOC-link", $"#{parentNode.Link}", parentNode.Title);
            if (parentNode.Nodes.Count > 0)
            {
                builder.OpenElement("ul", $"TOC TOC-{parentNode!.Level}");
                foreach (var node in parentNode!.Nodes)
                {
                    Console.WriteLine($" => Render {node!.Title} - {node.Level}");
                    GetNode(builder, node);
                }
                builder.CloseElement("ul");
            }
        }
        builder.CloseElement("li");
    }
}
