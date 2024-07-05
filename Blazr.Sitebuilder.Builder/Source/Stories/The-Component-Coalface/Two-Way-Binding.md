# Two Way Binding

This note is part of **The Component Coalface Series** and demonstrates how to use two way binding outside the `input` control setting.

The web is awash with articles and explanations on the basics.  I'll not regurgitate what you can read elsewhere here.  [The official Microsoft documentation article is here](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/data-binding).

Almost everything you read will be about the `input` context.  We'll broading that scope by showing how to build a dismissible alert with two way binding.

In a nutshell, two way binding is the simplest way to implement a two way communication channel between a parent and child component.  The parent provides [and changes] a parameter defined by the child component.  The child calls an `EventCallback` parameter to invoke a method in the parent passing back the new value.

## The First Attempt

The first attempt: you'll see this pattern in many community site questions.  

I've added some logging to show state.

```csharp
@if (this.Show)
{
    <div class="alert @this.AlertType alert-dismissible fade show" role="alert">
        @this.Message
        <button type="button" class="btn-close" @onclick="this.OnHide"></button>
    </div>
}

@code {
    [Parameter] public bool Show { get; set; }
    [Parameter] public MarkupString Message { get; set; } = new("<strong>Holy guacamole!</strong> You should check in on some of those fields below.");
    [Parameter] public string AlertType { get; set; } = "alert-danger";

    private void OnHide()
    {
        Console.WriteLine($"First Alert - Show: {this.Show}");
        this.Show = false;
        Console.WriteLine($"First Alert - Show Set To: {this.Show}");
    }
}
```

And the demo page

```csharp
@page "/"

<PageTitle>Home</PageTitle>

<h1>Alert Demo</h1>

<FirstAlert Show="_showFirstAlert" />

<div class="m-2">
    <button class="btn btn-primary" @onclick="ShowAlert" >Show Alerts</button> 
</div>

@code {
    private bool _showFirstAlert;

    private void ShowAlert()
    {
        Console.WriteLine($"Home - First Alert Show: {this._showFirstAlert}");
        _showFirstAlert = true;
        Console.WriteLine($"Home - First Alert Show Set To: {this._showFirstAlert}");

    }
}
```

The log when the application is run.

```text
==> Click the Home Show Button
Home - First Alert Show: False
Home - First Alert Show Set To: True
==> Click the Dismiss Button
First Alert - Show: True
First Alert - Show Set To: False
==> Subsequent clicks on the Home Show Button
Home - First Alert Show: True
Home - First Alert Show Set To: True
```

The crucial line is `Home - First Alert Show: True`.  On the first set, it's `false`, but on all sets it's `true`.  The dismiss button in the component set it to false, so why isn't it `false`?

The field/property in the parent and the property in the child are not instrinsically linked.  `SetParametersAsync`, sets the component property to the value provided in the `ParameterView` provided by the renderer.  For primitives this means setting the value and for objects the provided object reference. Within our component `Show` is set the value of the provided parameter at the time of setting.

`this.Show = false;` in `OnHide` has no effect on the value of `_showFirstAlert`.

The first click of *Show Alerts* changes the state of `_showFirstAlert`.  On subsequent renders at the completion of the button click UI event, the renderer detects the parameter change on `Alert1` and calls `SetParametersAsync` [which triggers a render of the component]. 

On any subsequent clicks there's no state change on `_showFirstAlert`.  The render sees no need to update `Alert1`'s state, so no call to `SetParametersAsync` and no render.

> This code breaks a fundimental component rule: Properties labelled as `Parameter`  should have a simple {set; get;} implementation.  You must not mutate them within the component: treat them as **ReadOnly**.

We need a mechanism to tell Home to update `_showFirstAlert`.

## The Second Attempt

```csharp
@if (this.Show)
{
    <div class="alert @this.AlertType show" role="alert">
        @this.Message
        <button type="button" class="btn-close" @onclick="this.OnHide"></button>
    </div>
}

@code {
    [Parameter] public bool Show { get; set; }
    [Parameter] public EventCallback<bool> ShowChanged { get; set; }
    [Parameter] public MarkupString Message { get; set; } = new("<strong>Holy guacamole!</strong> You should check in on some of those fields below.");
    [Parameter] public string AlertType { get; set; } = "alert-danger";

    private void OnHide()
    {
        this.ShowChanged.InvokeAsync(false);
    }
}
```

This:

1. Adds an `EventCallback<bool>` using a specific naming convention, so we now have a two way bind on `Show`.
2. Invokes the `EventCallback` in `OnHide` instead of setting `Value`.

The Demo page.

```csharp
@page "/"

<PageTitle>Home</PageTitle>

<h1>Alert Demo</h1>

<FirstAlert Show="_showFirstAlert" />

<SecondAlert Show="_showSecondtAlert" ShowChanged="this.OnSecondShowChanged" AlertType="alert-primary" />

<div class="m-2">
    <button class="btn btn-primary" @onclick="ShowAlerts" >Show Alerts</button> 
</div>

@code {
    private bool _showFirstAlert;
    private bool _showSecondtAlert;

    private Task OnSecondShowChanged(bool value)
    {
        _showSecondtAlert = false;
        return Task.CompletedTask;
    }

    private void ShowAlerts()
    {
        Console.WriteLine($"Home - First Alert Show: {this._showFirstAlert}");
        Console.WriteLine($"Home - Second Alert Show: {this._showSecondtAlert}");
        _showFirstAlert = true;
        _showSecondtAlert = true;
        Console.WriteLine($"Home - First Alert Show Set To: {this._showFirstAlert}");
        Console.WriteLine($"Home - Second Alert Show Set To: {this._showSecondtAlert}");
    }
 }
```

This wires up the two way binding manually so it's more obvious what's happening.

The log shows the state of `_showSecondtAlert`:

```text
...
Home - Second Alert Show: False
....
Home - Second Alert Show Set To: True
```

The state change is detected by the Renderer, so `SetParametersAsync` is called on the component.

## The Working Version

The show/hide mechanism now uses the `hidden` attribute.

```csharp

<div hidden="@(!this.Show)" class="alert @this.AlertType show">
    @this.Message
    <button type="button" class="btn-close" @onclick="this.OnHide"></button>
</div>

@code {
    [Parameter] public bool Show { get; set; }
    [Parameter] public EventCallback<bool> ShowChanged { get; set; }
    [Parameter] public MarkupString Message { get; set; } = new("<strong>Holy guacamole!</strong> You should check in on some of those fields below.");
    [Parameter] public string AlertType { get; set; } = "alert-danger";

    private void OnHide()
        => this.ShowChanged.InvokeAsync(false);
}
```

Binding is wired up more concisely like this:

```csharp
<SecondAlert @bind-Show="_showAlert" AlertType="alert-success" />
```

The full demo page.

```csharp
@page "/"

<PageTitle>Home</PageTitle>

<h1>Alert Demo</h1>

<FinalAlert @bind-Show="_showAlert" AlertType="alert-success" />

<div class="m-2">
    <button class="btn btn-success" @onclick="ShowAlert">Show Alert</button>
</div>

@code {
    private bool _showAlert;

    private void ShowAlert()
      => _showAlert = true;
}
```

## Wrap Up

Important points:

1. The is a *loose* linkage between a `Parameter` defined in a component definition and the property to which it is mapped in a component.  Setting a property to a new value will not set the parent's field to the same value.  This is a little more complicated with reference types.  Any mutation in the state of the object within the component will be reflected in the parent: they both hold the references to the same object.  However, if you create an assign a new object to a property in a component, the parent reference will still point to the old object.
 
1. You should not mutate `Parameter` properties in a component.
 
1. Binding can be used outside the `input` form context.  Once you've mastered this pattern there are many UI contexts where it's applicable.

