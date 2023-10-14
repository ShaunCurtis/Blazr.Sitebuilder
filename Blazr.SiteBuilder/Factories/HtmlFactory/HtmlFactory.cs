/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
namespace Blazr.SiteBuilder;


public partial class HtmlFactory
{
    private StringBuilder page = new StringBuilder();
     
    public string GetHtml()
        => page.ToString();
}
