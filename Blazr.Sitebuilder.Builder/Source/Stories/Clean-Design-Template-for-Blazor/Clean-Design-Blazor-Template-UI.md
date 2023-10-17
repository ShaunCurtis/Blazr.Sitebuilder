
The UI Razor files are re-organised into the following structure:

![Ui File Structure](https://shauncurtis.github.io/Design/assets/Blazr.Template.UI-File-Structure.png)
 
## Namespacing

All code files, including the razor files, are explicitly namespaced to `Blazr.Template.UI`.

`Counter.razor`:

```csharp
@page "/counter"
@namespace Blazr.Template.UI
<h1>Counter</h1>

<p>Current count: @currentCount</p>
```

## Components

Components are the heart of the Blazor UI.  Three terms are used for different component types:

1. *Routes* - are components with router attributes - mistakenly (in my view) called "Pages". They aren't classic web pages, so I don't refer to them as such.  `Index`, `Counter` and `FetchData` are all Routes. 
2. *Views* - these are high level components, often forms.  In this code the WeatherForecastList' is a view.
3. *Controls* are low level components containing markup and razor code that define re-usable code blocks.

## Controls

### LoadingControl

`LoadingControl` displays a loading message or a spinner when the page is loading it's data, and only displays the data when it's loaded.  It displays a animated spinner while `IsLoaded` is false and only displays `ChildContent` when `IsLoaded` is true.  It removes the need for a lot of error checking code in components. 

### PagingControl

`PagingControl` displays a paging bar.  It's simplistic, designed to demo component interaction.

There are a few key concepts it does implement:

1. Setting a local loop variable.  The code snippet shows setting `pageno` to the page loop variable, and then using the local variable in any loop code.  If you use `page` in `onclick`, when the click event occurs the looping has completed and `page` is set at `_pagingData.LastPage` plus one.

```csharp
@for (int page = 0; page <= _pagingData.LastPage; page++)
{
    var pageno = page;
    var viewpageno = page + 1;
    <button type="button" class="btn @GetCss(pageno)" @onclick="() => this.GotToPage(pageno)">@viewpageno</button>
}
 ```

 2. Implementing `IDisposable` in a component, and handling events.

 3. Re-rendering on an event.  This one is conditional.  If the control triggered the page change event then `_currentpage == _pagingData.Page` and nothing happens.  `this.InvokeAsync(this.StateHasChanged);` isn't strictly needed in this instance, but if the event is triggered outside UI thread, this ensures thread safety - `InvokeAsync` is a `ComponentBase` method that invokes the action on the UI Thread.

```csharp
private void OnPageChanged(object? sender, EventArgs e)
{
    if (_currentpage != _pagingData.Page)
    {
        _currentpage = _pagingData.Page;
        this.InvokeAsync(this.StateHasChanged);
    }
}
```

4. If a Parameter is required.  Throw an error if it's not set in `SetParametersAsync`. if you implement `SetParametersAsync` you must set the component parameters immediately as shown, and then call the base `SetParametersAsync` with an empty `ParameterView`.
```csharp
public override Task SetParametersAsync(ParameterView parameters)
{
    parameters.SetParameterProperties(this);
    if (PagingData is null)
        throw new ApplicationException("Paging Control must have a PagingData object assigned");
    return base.SetParametersAsync(ParameterView.Empty);
}
```

5. With *Nullable** enabled, references to `PagingData` will throw warnings.  To prevent this a local global variable `_pagingData` is used throughout the component.  It's mapped to `PagingData` using a *null forgiving* assignment.

```csharp
private PagingData _pagingData => PagingData!;
```

### WeatherForecastList

`WeatherForecastList.razor` is a re-usable compoinent containing all the code from `FetchData`.  The component:

1. Injects the View Service.
2. Uses a local reference to the View Service to handklle nullable issues.
2. Has no local data.  It's all in the View Service.
3. Uses `LoadingControl` tied to the View Service `IsLoaded`.
4. Registers for any ListChange events, and re-renders the component when one occurs.

## A Look at How Paging Works

It's isn't obvious from `WeatherForecastList` how it updates when a new page is clicked in the `PagingControl`.

![Pagination](https://shauncurtis.github.io/Design/assets/Pagination.png)

Clicking a page button Sets the new page number in `PagingData` and triggers `PagingData.PageChanged`.  This triggers `WeatherForecastViewService.OnPageChanged` event handler that gets the new paged dataset.  This triggers the `WeatherForecastViewService.ListChanged` event.  `WeatherForecastList` is register for this event.  It triggers `StateHasChanged` which re-renders the component.

Meanwhile in `PagingControl`, which triggered the whole sequence, the new page number has been set in `PagingData` before the event handler completes and calls `StateHasChanged`.

On first render `PagingControl` is inside `LoadingControl`, so is only rendered when loading is complete.

Note the page number is maintained across page navigations.  You can go to `Counter` and back to `FetchData`.  However, if you refresh the SPA then the state is lost.

## Apps

Apps contains the root application component `App`.  It's vanilla and namespaced.

```csharp
@namespace Blazr.Template.UI

<Router AppAssembly="@typeof(App).Assembly" PreferExactMatches="@true">
    <Found Context="routeData">
        <RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" />
    </Found>
    <NotFound>
        <LayoutView Layout="@typeof(MainLayout)">
            <p>Sorry, there's nothing at this address.</p>
        </LayoutView>
    </NotFound>
</Router>
```

