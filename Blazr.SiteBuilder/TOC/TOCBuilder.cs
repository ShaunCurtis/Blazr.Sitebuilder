/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

namespace Blazr.SiteBuilder;

public static class TOCBuilder
{
    public static string? BuildTOC(PageTOC toc)
    {
        var builder = new HtmlFactory();

        builder.Add("h4", "p-2", "Table of Contents");
        builder.OpenElement("ul", "TOC");
        {
            Console.WriteLine($" => Render {toc!.Title} - {toc.Level}");
            GetNode(builder, toc);
        }
        builder.CloseElement("ul");

        return builder.GetHtml();
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

    //public static void Build(RenderTreeBuilder builder, PageTOC toc)
    //{
    //    builder.OpenElement(0, "h4");
    //    builder.AddAttribute(1, "class", "p-2");
    //    builder.AddContent(2, "Table of Contents");
    //    {
    //        builder.OpenElement(0, "ul");
    //        builder.AddAttribute(1, "class", "TOC");

    //        GetNode(builder, toc);

    //        builder.CloseElement();
    //    }
    //    builder.CloseElement();

    //}

    //private static void GetNode(RenderTreeBuilder builder, PageTOC? parentNode)
    //{
    //    builder.OpenElement(0, "li");
    //    builder.AddAttribute(1, "class", $"TOC-item TOC-item-{parentNode!.Level}");

    //    // Add the parent node anchor
    //    if (!parentNode.Hidden)
    //    {
    //        builder.OpenElement(2, "a");
    //        builder.AddAttribute(3, "class", "TOC-link");
    //        builder.AddAttribute(4, "href", $"#{parentNode.Link}");
    //        builder.AddContent(5, parentNode.Title);
    //        builder.CloseElement();
    //    }

    //    // If we have sub nodes then create a new ul and iterate
    //    if (parentNode.Nodes.Count > 0)
    //    {
    //        builder.OpenElement(6, "ul");
    //        builder.AddAttribute(7, "class", $"TOC TOC-{parentNode!.Level}");

    //        foreach (var node in parentNode!.Nodes)
    //        {
    //            GetNode(builder, node);
    //        }

    //        builder.CloseElement();
    //    }

    //    builder.CloseElement();
    //}
}
