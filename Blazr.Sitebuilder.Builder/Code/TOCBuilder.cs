using Microsoft.AspNetCore.Components.Rendering;

namespace Blazr.Sitebuilder.Builder.Code;

public static class TOCBuilder
{
    public static void Build(RenderTreeBuilder builder, PageTOC toc)
    {

    }

    private static void GetNode(RenderTreeBuilder builder, PageTOC? parentNode)
    {
        builder.OpenElement(0, "li");
        builder.AddAttribute(1, "class", $"TOC-item TOC-item-{parentNode!.Level}");
        
        // Add the parent node anchor
        if (!parentNode.Hidden)
        {
            builder.OpenElement(2, "a");
            builder.AddAttribute(3, "class", "TOC-link");
            builder.AddAttribute(4, "href", $"#{parentNode.Link}");
            builder.AddContent(5, parentNode.Title);
            builder.CloseElement();
        }
        //builder.AddAttribute();
        builder.CloseElement();
    }

    private void GetNode(HtmlFactory builder, PageTOC? parentNode)
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
