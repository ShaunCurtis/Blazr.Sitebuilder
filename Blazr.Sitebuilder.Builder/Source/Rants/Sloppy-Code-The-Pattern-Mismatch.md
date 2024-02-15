
It's interesting what compilers will and will not let you get away with.

Here's the answer to a recent Stack Overflow question on adding an onclick event to a component.

```csharp
<div @onclick="OnClick">
    @ChildContent
</div>

public partial class DataRow
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public EventCallback OnClick { get; set; }
}
```

The code looks clean and succinct.  Where's the problem?

Consider this:

`@onclick` is expecting this pattern.

```csharp
Task EventHandler(MouseEventArgs e)
```

And we're giving it:

```csharp
void EventCallback()
```

The Razor compiler doesn't complain and just wires it up.  And it works.  

Well sort of.  But if you apply this pattern more generally you will see unpredicated UI behaviour:  UI updates will happen out of the sequence you expect.

Let's look at a correct implementation.

```csharp
<div @onclick="this.OnClicked">
    @ChildContent
</div>

@code {
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public EventCallback OnClick { get; set; }

    private async Task OnClicked(MouseEventArgs e)
        => await OnClick.InvokeAsync();
}
```

We've:

1. Provided a `Task` back to the UI event handler so it can handle `StateHasChanged` calls correctly on async yielding. 
1. Sunk the provided `MouseEventArgs`.
1. Called `OnClick` asynchronously and awaited it's completion.

The moral of this rant is: Don't write sloppy code.  Been there, done that, got the scars!