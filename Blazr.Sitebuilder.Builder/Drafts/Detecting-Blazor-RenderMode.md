Net8 introduced the Rendermode, and with it the ability to create all sorts of variations of static. active and client side rendered pages.

The problem with this choice is it's very easy to get things wrong.  I'm sure people will shoot themselves in the foot frequently.  It will be a rich source for articles on the topic and a common FAQ topic on the forums.

The way the current setup works means that you often need to detect the render mode to react correctly in components and provide a seamless UX.  Having pages flicker as they are rendered isn't good.

In this article I'll look at two such UX experiences: the startup flash screen and the slow loading page.

## Detecting the Render Mode

In the old pre-Net8.0 world you were in either Server or WASM mode.  You detect pre-rendering by checking the state of the `IHttpContextAccessor`.

That's not so easy where you can switch between `InteractiveServer` to `InteractiveWebAssembly`.  `IHttpContextAccessor` isn't relevant in the *WASM* context.

The solution is to create a `IBlazrRenderStateService` with both server and client implementations.

```csharp
public interface IBlazrRenderStateService
{
    public Guid ServiceUid { get; }
    public BlazrRenderState RenderState { get; }
    public bool IsPreRender => this.RenderState == BlazrRenderState.PreRender;
}
```

```csharp
public enum BlazrRenderState
{
    None,
    PreRender,
    SSR,
    CSR
}
```

The Server implementation:

```csharp
namespace Blazr.RenderState.Server;

public class ServerRenderStateService : IBlazrRenderStateService
{
    private IHttpContextAccessor _httpContextAccessor;

    public Guid ServiceUid { get; } = Guid.NewGuid();

    public BlazrRenderState RenderState =>
        !(_httpContextAccessor.HttpContext is not null && _httpContextAccessor.HttpContext.Response.HasStarted)
            ? BlazrRenderState.PreRender
            : BlazrRenderState.SSR;

    public ServerRenderStateService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
}
```

The Client implementation:

```csharp
namespace Blazr.RenderState.WASM;

public class WASMRenderStateService : IBlazrRenderStateService
{
    public Guid ServiceUid { get; } = Guid.NewGuid();

    public BlazrRenderState RenderState => BlazrRenderState.CSR;
}
```

THese are all split into three projects because of WASM build library incompatibilities.

There are two static builder methods to configure the necessary services:

```csharp
namespace Blazr.RenderState.Server;

public static class RenderStateServerServices
{
    public static void AddBlazrRenderStateServerServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IBlazrRenderStateService, ServerRenderStateService>();
    }
}
```

```csharp
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Blazr.RenderState.WASM;

public static class RenderStateWASMServices
{
    public static void AddBlazrRenderStateWASMServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<IBlazrRenderStateService, WASMRenderStateService>();
    }
}
```


You can see the full implemnentation in *https://github.com/ShaunCurtis/Blazr.RenderState/*.

## Slow Loading in an InteractiveServer Per Page configuration 

```csharp
@page "/"
@rendermode InteractiveServer
@using Blazr.RenderState
<PageTitle>Home</PageTitle>

<h1>Hello, world!</h1>

Welcome to your new app.

@if (_loading)
{
    <div class="alert alert-warning">Loading</div>
    return;
}


<div class="alert alert-success">Loaded</div>

@code {
    private bool _loading = true;

    protected override async Task OnInitializedAsync()
    {
        // emulate slow data load
        await Task.Delay(2000);
        _loading = false;
    }
}
```

When the application first loads notice:

1. A lag as the page pre-renders.
1. Loaded displayed as the pre-render completes.
1. Loading displayed as the page loads interactively.
1. Loaded after the completion of the await.
1. If you go to another page and then navigate back you will get the same effect, except in step 1 it will lag on the page you are leaving.

The reason for this is that the application is set to Per page/component and the router is being rendered statically on the server. In App it's render mode is not set

    <Routes />
It's an Satic SSR component.

That means that App, Router, MainLayout, NavMenu are all SSSR. It's only Home and it's sub-components that are ASSR.

This version of the page loads immediately and the SSSR to ASSR context switch is much more seamless.

```csharp
@page "/Loader"
@rendermode InteractiveServer
@using Blazr.RenderState
@inject IBlazrRenderStateService RenderState
<PageTitle>Home</PageTitle>

<h1>Hello, world!</h1>

Welcome to your new app.

@if (RenderState.IsPreRender || _loading)
{
    <div class="alert alert-warning">Loading</div>
    return;
}


<div class="alert alert-success">Loaded</div>

@code {
    private bool _loading = true;

    protected override async Task OnInitializedAsync()
    {
        // exist early
        if (this.RenderState.IsPreRender)
            return;

        await Task.Delay(2000);
        _loading = false;
    }
}
```

## The Flash page on Rendering a WebAssembly Page

```csharp
<div class="alert alert-primary m-5">
    BIG SPLASH
</div>
```

```csharp
@using Blazr.RenderState
@inject IBlazrRenderStateService RenderState

@if (RenderState.IsPreRender)
{
    <Splash />
    return;
}

<Router AppAssembly="@typeof(Program).Assembly">
    <Found Context="routeData">
        <RouteView RouteData="@routeData" DefaultLayout="@typeof(Layout.MainLayout)" />
        <FocusOnNavigate RouteData="@routeData" Selector="h1" />
    </Found>
</Router>
```

