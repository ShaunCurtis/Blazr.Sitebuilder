/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================

using System.Text.RegularExpressions;

namespace Blazr.SiteBuilder;

public static class TOCUtils
{
    public static PageTOC GetTOC(string content)
    {
        var matchString = $"<h([12])\\sid=\"(.*)\"[^>]*>(.+)<\\/h[\\d]>";
        var regex = new Regex(matchString);
        var matches = regex.Matches(content);

        var root = new PageTOC() { Level = 0, Title = "Top" };
        ProcessMatches(matches, root, 0);
        return root;
    }

    private static int ProcessMatches(MatchCollection matches, PageTOC parentToc, int matchIndex)
    {
        var index = matchIndex;
        var newLevel = 0;
        var thisLevel = parentToc.Level + 1;
        PageTOC? Toc = null;

        while (matches.Count > index)
        {
            var match = matches[index];

            if (match.Groups.Count == 4 && int.TryParse(match.Groups[1].Value, out newLevel))
            {
                if (newLevel < thisLevel)
                    return index;
                if (newLevel > thisLevel && Toc is not null)
                {
                    index = ProcessMatches(matches, Toc, index);
                }
                else if (newLevel > thisLevel && Toc is null)
                {
                    Toc = new PageTOC() { Level = thisLevel, Link = String.Empty, Title = "No Initial H1 found", Hidden = true };
                    parentToc.Nodes.Add(Toc);
                    index = ProcessMatches(matches, Toc, index);
                }
                else
                {
                    Toc = new PageTOC() { Level = newLevel, Link = match.Groups[2].Value, Title = match.Groups[3].Value };
                    parentToc.Nodes.Add(Toc);
                    index++;
                }
                newLevel = thisLevel;
            }
        }
        return index;
    }
}