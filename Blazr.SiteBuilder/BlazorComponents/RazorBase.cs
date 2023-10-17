/// ============================================================
/// Author: Shaun Curtis, Cold Elm Coders
/// License: Use And Donate
/// If you use it, donate something to a charity somewhere
/// ============================================================
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazr.SiteBuilder;

public abstract class RazorBase
{
    protected virtual void BuildRenderTree(RenderTreeBuilder builder) { }
}
