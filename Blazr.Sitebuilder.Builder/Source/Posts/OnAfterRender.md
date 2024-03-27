In my [humble] opinion, 90+% of code in `OnAfterRender{Async}` shouldn't be there.  In this article I'll explain why.

So, what should be there?  

JSInterop code.  That's about it.  If your code needs to interact with the browser through JS, then it's in the right place.

Before jumping into the code, some important relevant facts:

1. `OnAfterRender{Async}` is a UI event.  It's raised by the Renderer when the browser signals that a UI update has completed.  What is isn't is part of the component lifecycle methods.  That's an extremely important distinction.  The calls to `StateHasChanged` in the component lifecycle may ultimately trigger `OnAfterRender{Async}`, but that's just a consequence of an action.  There's no direct linkage, and therefore no control over when `OnAfterRender{Async}` is executed by the *synchronisation context*.

2. The *Blazor Synchronisation Context* prioritizes delegates *Posted* to it's queue over UI event delegates.  It effectively has two queues: the *Post* queue and the UI queue.  The lifecycle code is posted to the *Post* queue, `OnAfterRender{Async}` is queued on the UI event queue. 

The demo console logging component is shown in full at the end of the article.

## A Busy Synchronisation Context

In this section an async method with a minimal delay.

```csharp
    protected async override Task OnInitializedAsync()
    {
        //...
        await Task.Delay(1);
        //...
    }
```

The page runs as follows:

```text
1. 735bf90a => OnInitialized Completed.
2. 735bf90a => OnInitializedAsync Started.
3. 735bf90a => Component Rendered.
4. 735bf90a => OnInitializedAsync Completed.
5. 735bf90a => OnParametersSet Completed.
6. 735bf90a => OnParametersSetAsync Completed.
7. 735bf90a => ShouldRender.
8. 735bf90a => Component Rendered.
9. 735bf90a => First OnAfterRender Completed.
10. 735bf90a => First OnAfterRenderAsync Completed.
11. 735bf90a => Subsequent OnAfterRender Completed.
12. 735bf90a => Subsequent OnAfterRenderAsync Completed.
```

Note that the component rendered at steps 3 and 8, but the two consequential `OnAfterRender{Async}` events ran at the end of the sequence [9-12].

This happened because the *synchronisation context* post queue remained busy. The Task.Delay continuation was posted to the *synchronisation context* before it completed it's queued work. It continued processing it's queue, including the continuation, until it was empty.  `OnAfterRender{Async}` was in the slow lane and only processed when the post queue was empty.

## An Idle Synchronisation Context

In this section there's a longer delay.

```csharp
    protected async override Task OnInitializedAsync()
    {
        //...
        await Task.Delay(100);
        //...
    }
```

The page runs as follows:

```text
1. 53772c02 => OnInitialized Completed.
2. 53772c02 => OnInitializedAsync Started.
3. 53772c02 => Component Rendered.
4. 53772c02 => First OnAfterRender Completed.
5. 53772c02 => First OnAfterRenderAsync Completed.
6. 53772c02 => OnInitializedAsync Completed.
7. 53772c02 => OnParametersSet Completed.
8. 53772c02 => OnParametersSetAsync Completed.
9. 53772c02 => ShouldRender.
10. 53772c02 => Component Rendered.
11. 53772c02 => Subsequent OnAfterRender Completed.
12. 53772c02 => Subsequent OnAfterRenderAsync Completed.
```

Note the initial `OnAfterRender{Async}` is now run immediately after the render at 3 and 4.

The change happens because the *synchronisation context* completes all the queued work and can service the UI event queue before the Task.Delay awaiter finally completes and the continuation is scheduled. 

### Implications

1. You don't control how long awaiters wait [or even if they wait at all] : you have no control on when `OnAfterRender{Async}` runs.

2. The output demonstrates the consequences of trying to run logic code in `OnAfterRender{Async}`.  A different delay leads to different output.  The lifecycle methods almost certainly mutate the component state, and that state will be different at the two execution points.

3. Changing state in `OnAfterRender{Async}` means you need to execute one more call to `StateHasChanged` to update the UI to the current component state.  Why not just execute the code in the lifecycle methods. 

## Pre-Rendering

Net8 introduced the RenderMode and brought pre-rendering to the for.  One interesting facet of this is that `OnAfterRender{Async}` isn't called during pre-render: there's no browser context.  That's makes it an obvious target for code, such as relatively slow external data fetches, that you don't want to run during pre-render.

Don't fall into the trap.  The reasons for not doing it highlighted above still hold.

In Net8, you can detect component pre-rendering like this:

```csharp
@code {
    [CascadingParameter] HttpContext? HttpContext { get; set; }

    private bool _isPreRender;

    protected override void OnInitialized()
    {
        _isPreRender = this.HttpContext is not null;
    }
}
```

The `HttpContext` is a root level cascade that is only exists when the component is pre-rendering.  In interactive rendering mode, it's `null`.


## Takeaways

To summarise what does and doesn't belong in `OnAfterRender{Async}`:

 - If your code needs to interact with the browser through JS, it's in the right place.
 - If your code mutates the component state necessitating a render, it belongs elsewhere in `OnInitialized{Async}\OnParametersSet{Async}`. 


## Appendix - The Demo Component

The following component helps document the process.  It logs when code is executed on the *synchronisation context*.

```csharp
@page "/"

@{ Console.WriteLine($"{_componentUid} => Component Rendered."); }

<h1>OnAfterRender Rendering</h1>

<div class="bg-dark text-white mt-5 m-2 p-2">
    <pre>@value</pre>
</div>

@code {
    private string _componentUid = Guid.NewGuid().ToString().Substring(0, 8);
    private string value = string.Empty;

    protected override void OnInitialized()
    {
        Console.WriteLine($"{_componentUid} => OnInitialized Completed.");
    }

    protected async override Task OnInitializedAsync()
    {
        Console.WriteLine($"{_componentUid} => OnInitializedAsync Started.");
        value = $"1";
        await Task.Delay(1);
        value = $"{value}=>2";
        Console.WriteLine($"{_componentUid} => OnInitializedAsync Completed.");
    }

    protected override void OnParametersSet()
    {
        Console.WriteLine($"{_componentUid} => OnParametersSet Completed.");
    }

    protected override Task OnParametersSetAsync()
    {
        Console.WriteLine($"{_componentUid} => OnParametersSetAsync Completed.");
        return Task.CompletedTask;
    }

    protected override bool ShouldRender()
    {
        Console.WriteLine($"{_componentUid} => ShouldRender.");
        return true;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            Console.WriteLine($"{_componentUid} => First OnAfterRender Completed.");
            value = $"{value}=>3";
        }
        else
            Console.WriteLine($"{_componentUid} => Subsequent OnAfterRender Completed.");

    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            Console.WriteLine($"{_componentUid} => First OnAfterRenderAsync Completed.");
        else
            Console.WriteLine($"{_componentUid} => Subsequent OnAfterRenderAsync Completed.");

        return Task.CompletedTask;
    }
}
```


