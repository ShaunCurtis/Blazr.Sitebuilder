Registration is the process by which a child component registers itself with a parent component.  The actual content is built out by the parent based on data provided by the child.

I've used a `Select` edit control as a somewhat contrived example.  There's no real reason to register select options, but it provides a simple framework to demonstrate the principles.

The Repo for this article is here - [Blazr.ComponentRegistration](https://github.com/ShaunCurtis/Blazr.ComponentRegistration)

### Loose Coupling

You will see similar examples of this pattern where the parent component cascades itself and the child components call a register method on the parent, often registering themselves.

A personal view, but I don't believe this is good practice for the following reasons.

1. You are tightly coupling components together.
1. You are passing around references to objects you don't control or manage.
1. You are passing around objects that expose functionality that shouldn't be used outside the context of the Renderer. 

### Defer

The main component uses the `Defer` component to render it's content.

It looks like:

```csharp
@ChildContent

@code {
   [Parameter] public RenderFragment? ChildContent { get; set; }
}
```

It's purpose is to defer rendering of the actual content until after the `Option` components have registered.  `Defer` is at the same level in the Render Tree as the `Option` components, so renders in sequence with them.  As it's placed last, it renders it content - provided as a `RenderFragment` from the parent - last.

## Using RenderFragments

Our definition looks like this:

```csharp
<div class="mb-3">
    <OptionSelect @bind-Value="_country">
        <Option Id="UK" Value="UK" />
        <Option Id="France" Value="France" />
        <Option Id="Spain" Value="Spain" />
        <Option Id="Portugal" Value="Portugal" />
    </OptionSelect>
</div>
```

### The Option Component

1. Everything happens in `SetParametersAsync` before any rendering takes place.
1. The code within `SetParametersAsync` only runs once when `_hasRegistered` is `false`.
1. Exceptions are raised if there's any missing data.
1. The component registers the `OptionBuilder` method as the RenderFragment when it calls Register. 
1. `SetParametersAsync` returns a completed Task.  It short circuits the lifecycle process: It does nothing so there's no point in running it.


```csharp
@namespace Blazr.ComponentRegistration.Components
@using Microsoft.AspNetCore.Components.Rendering

@code {
    private bool _hasRegistered;

    [Parameter, EditorRequired] public string? Id { get; set; }
    [Parameter, EditorRequired] public string? Value { get; set; }
    [CascadingParameter] private Action<RenderFragment>? Register { get; set; }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        // We only need to register once.
        // We can ignore all subsequent parameter changes, short circuit the lifecycle processes
        // and return a completed task.
        if (!_hasRegistered)
        {
            parameters.SetParameterProperties(this);

            // Check we have everything.  If not throw an exception.
            ArgumentNullException.ThrowIfNull(this.Id);
            ArgumentNullException.ThrowIfNull(this.Value);
            ArgumentNullException.ThrowIfNull(this.Register);

            // Register our render fragment
            this.Register.Invoke(OptionBuilder);
            _hasRegistered = true;
        }
        // Short circuit the life cycle process.  We waste processor time doing it for no purpose.
        return Task.CompletedTask;
    }

    //Create the render fragment that is the rendered content
    private void OptionBuilder(RenderTreeBuilder __builder)
    {
        <option value="@this.Id">@this.Value</option>
    }
}
```

### The OptionSelect Component

1. It cascades the Register method to the child content.
2. If defers rendering the `select` with the registered `RenderFragments` until after all the child components have registered. 

```csharp
@namespace Blazr.ComponentRegistration.Components

<CascadingValue Value="Register" IsFixed>
    @ChildContent
</CascadingValue>

<Defer>
    <select class="form-select"
            @bind:get="@this.Value"
            @bind:set="this.SetValue">

        @if (this.Value is null)
        {
            <option selected disabled value=""> -- Select An Item -- </option>
        }

        @foreach (var item in _items)
        {
            @item
        }
    </select>

</Defer>

@code {
    [Parameter] public string? Value { get; set; }
    [Parameter] public EventCallback<string?> ValueChanged { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    private List<RenderFragment> _items = new();

    private void Register(RenderFragment option)
    {
        if (!_items.Contains(option))
            _items.Add(option);
    }

    private async Task SetValue(string? value)
        => await this.ValueChanged.InvokeAsync(value);
}
```

### Demo Page

```csharp
@page "/"

<PageTitle>Home</PageTitle>

<div class="mb-3">
    <OptionSelect @bind-Value="_country">
        <Option Id="UK" Value="UK" />
        <Option Id="France" Value="France" />
        <Option Id="Spain" Value="Spain" />
        <Option Id="Portugal" Value="Portugal" />
    </OptionSelect>
</div>

<div class="alert alert-primary">Country: @_country</div>

@code {
    private string? _country;
}
```

## Using a Context.

In more complex situations we can use a data object for the data transfer and a context class to manage registration and collection management. 

## The BlazrOptionContext

A simple `record` or `readonly struct` value object to hold the option data.

```csharp
public record OptionData(string Id, string Value);
```

The context, which in this case just provides the registration process method and exposes a public readonly collection of `OptionData` object.  It provides the functionality we need.

```csharp
public class OptionContext
{
    private List<OptionData> _items = new List<OptionData>();

    public IEnumerable<OptionData> Items => _items.AsEnumerable();

    public void Register(OptionData option)
    {
        if (!_items.Contains(option))
            _items.Add(option);
    }
}
```

## BlazrOption Component

The sole purpose of the component is to register its configuration data.  Nothing else.  There's no content to output to the DOM.

1. Everything happens in `SetParametersAsync`.
1. The code within `SetParametersAsync` only runs once when `_hasRegistered` is `false`.
1. The Parameters are set manually.  It's much faster and relatively simple to code when you only have a few.
1. Exceptions are raised if there's any missing data.
1. The component registers an `OptionData` with the context: it's data, not itself.
1. `SetParametersAsync` returns a completed Task.  It short circuits the lifecycle process: there's no point in running it to do nothing.

```csharp
using Microsoft.AspNetCore.Components;

namespace Blazr.ComponentRegistration.Components;

public class BlazrOption : ComponentBase
{
    private bool _hasRegistered;

    [Parameter, EditorRequired] public string? Id { get; set; }
    [Parameter, EditorRequired] public string? Value { get; set; }
    [CascadingParameter] private BlazrOptionContext? Context { get; set; }

    public override Task SetParametersAsync(ParameterView parameters)
    {
        // We only need to do anything if we haven't yet registered
        if (!_hasRegistered)
        {
            // Manually get our parameters from the ParameterView
            var id = parameters.GetValueOrDefault<string>("Id");
            var value = parameters.GetValueOrDefault<string>("Value");
            this.Context = parameters.GetValueOrDefault<BlazrOptionContext>("Context");

            // Check we have everything.  If hot throw an exception
            ArgumentNullException.ThrowIfNull(id);
            ArgumentNullException.ThrowIfNull(value);
            ArgumentNullException.ThrowIfNull(Context);

            // Register
            this.Context.Register(new(id, value));
            _hasRegistered = true;
        }
        // Short circuit the Lifecycle process.  We're wasting processor time doing it for no purpose.
        return Task.CompletedTask;
    }
}
```

## BlazrSelect Component

The main component creates an instance of the `OptionContext` and cascades it to the `ChildContent` - the `BlazorOption` components.  It only does this on the first render.  Their only purpose is to register their data.

The `Defer` component is used as before to defer rendering of the main component content.  This time the component builds the `option` code directly.

```csharp
@namespace Blazr.ComponentRegistration.Components

@if (_firstRender)
{
    <CascadingValue Value="_optionContext" IsFixed>
     @ChildContent
    </CascadingValue>
}

<Defer>
    <select class="form-select"
            @bind:get="@this.Value"
            @bind:set="this.SetValue">

        @if (this.Value is null)
        {
            <option selected disabled value=""> -- Select An Item -- </option>
        }

        @foreach (var item in _optionContext.Items)
        {
            <option value="@item.Id">@item.Value</option>
        }
    </select>

</Defer>

@code {
    [Parameter] public string? Value { get; set; }
    [Parameter] public EventCallback<string?> ValueChanged { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    private readonly BlazrOptionContext _optionContext = new();
    private bool _firstRender = true;

    private async Task SetValue(string? value)
        => await this.ValueChanged.InvokeAsync(value);
}
```

## Index Demo

And here's the demo page:

```csharp
@page "/"

<PageTitle>Home</PageTitle>

<div class="mb-3">
    <BlazrSelect @bind-Value="_country">
        <BlazrOption Id="UK" Value="UK"/>
        <BlazrOption Id="France" Value="France" />
        <BlazrOption Id="Spain" Value="Spain" />
        <BlazrOption Id="Portugal" Value="Portugal" />
    </BlazrSelect>
</div>

<div class="alert alert-primary">Country: @_country</div>

@code {
    private string? _country;
}
```