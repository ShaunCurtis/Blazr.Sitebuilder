## Overview

This article looks at how to handle large lists in Blazor, decouple the data from the UI, and use the *Notification Pattern* to trigger updates in components.

The starting solution for this article is the out-of-the-box Blazor Server template.

## Repo

The Github Repository for this project is [Blazr.Articles.Lists](https://github.com/ShaunCurtis/Blazr.Articles/tree/main/Blazr.Articles.Lists)

## Refactoring the Project

Our first step is to separate the code into three principle directory structures: Data, Core and UI.  Normally I would use three projects, but I'm keeping things simple in this article.  These represent the three primary domains in the simple Clean Design model.  We'll re-distribute the code as we progress.  Code in each domain resides in the domain namespace:  for example all *Core* code resides in `Blazr.Articles.Core`.

### WeatherForecast

Move to *Core* and:

1. Change it to a `record` value object.
2. Add a `WeatherForecastId` property as a `Guid`.
3. Change all the properties to `{get; init;}`.

```csharp
namespace Blazr.Articles.Core;

public record WeatherForecast
{
    public Guid WeatherForecastId {get; init;} = Guid.Empty;
    public DateTime Date { get; init; }
    public int TemperatureC { get; init; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public string? Summary { get; init; }
}
```

### ListOptions

Add a `ListOptions` class to *Core*.  This class contains paging data: it's passed in any data pipeline request to define the dataset "page" to retrieve.  Code should never retrieve unconstrained lists from data sources - you never know how big the data set may grow.  Set the defaults to sensible maximum values.  The class has various constructors and setters we'll use later.

```csharp
using Microsoft.AspNetCore.Components.Web.Virtualization;
namespace Blazr.Articles.Core;

public class ListOptions
{
    public int StartRecord { get; set; } = 0;
    public int PageSize { get; set; } = 1000;
    public int ListCount { get; set; }
    public int Page => this.StartRecord / this.PageSize;

    public ListOptions() { }

    public ListOptions(int startRecord, int pageSize)
    {
        this.StartRecord = startRecord;
        this.PageSize = pageSize;
    }

    public void Set(ListOptions options)
    {
        this.PageSize = options.PageSize;
        this.StartRecord = options.StartRecord;
    }

    public void Set(ItemsProviderRequest options)
    {
        this.PageSize = options.Count;
        this.StartRecord = options.StartIndex;
    }

    public void SetPage(int pageno)
        =>  this.StartRecord = pageno * this.PageSize;

    public ListOptions Copy => new ListOptions { 
        StartRecord = this.StartRecord, 
        PageSize = this.PageSize, 
        ListCount = this.ListCount
    };
}
```

### WeatherForecastDataStore

Rename the `WeatherForcastService` to `WeatherForecastDataStore` and restructure it as follows.

We:

1. Generate a large data set on instantiation and assign it to an internal field.
2. Add various public methods for List and CRUD operations.
3. Maintain an internally consistent data set: data requests are passed copies, not references to internal objects.
4. The data pipeline methods are all `ValueTask` based because they emulate real world asynchronous database operations.

```csharp
namespace Blazr.Articles.Data;
using Blazr.Articles.Core;

public class WeatherForecastDataStore
{
    // Internal list to hold the data set.
    private List<WeatherForecast> _records;

    public WeatherForecastDataStore()
    {
        _records = GetForecasts();
    }

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public ValueTask<bool> DeleteForecastAsync(Guid Id) {
        var record = _records.FirstOrDefault(item => item.WeatherForecastId == Id);
        if (record is not null)
            _records.Remove(record);
        return ValueTask.FromResult(record is not null);
    }

    public ValueTask<bool> SaveForecastAsync(WeatherForecast record)
    {
        var isrecord = _records.Any(item => item.WeatherForecastId == record.WeatherForecastId);
        if (isrecord)
            _records.Remove(record);
        _records.Add(record with { });
        return ValueTask.FromResult(isrecord);
    }

    public ValueTask<List<WeatherForecast>> GetForecastsAsync(ListOptions options)
    {
        var list = _records
            .OrderBy(item => item.Date)
            .Skip(options.StartRecord)
            .Take(options.PageSize)
            .ToList();

            var newList = new  List<WeatherForecast>();
            list.ForEach(item => newList.Add(item with {}));

            return ValueTask.FromResult(newList);
    }

    public ValueTask<int> GetForecastCountAsync()
        => ValueTask.FromResult(_records.Count);

    private List<WeatherForecast> GetForecasts()
    {
        return Enumerable.Range(1, 200).Select(index => new WeatherForecast
        {
            WeatherForecastId = Guid.NewGuid(),
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToList();
    }
}
```

### IWeatherForecastDataBroker

`IWeatherForecastDataBroker` defines the interface for the data pipeline connection between Core and Data domain code.  Add `IWeatherForecastDataBroker` to *Core*.  It defines the basic List and CRUD operations we implement for this article:

1. Delete a record.
2. Save a record.
3. Get a record list.
4. Get the total number of records.

Note the simularity with the classic Repository pattern.

```csharp
namespace Blazr.Articles.Core;

public interface IWeatherForecastDataBroker
{
    public ValueTask<List<WeatherForecast>> GetForecastsAsync(ListOptions options);
    public ValueTask<int> GetForecastCountAsync();
    public ValueTask<bool> SaveForecastAsync(WeatherForecast record);
    public ValueTask<bool> DeleteForecastAsync(Guid Id);
}
```

### WeatherForecastServerDataBroker

`WeatherForecastServerDataBroker` is the concrete server based implementation of `IWeatherForecastDataBroker`.  It's commonly known as a "shim": a thin call through layer into the data store.  Add `WeatherForecastServerDataBroker` to *Data*.

```csharp
using Blazr.Articles.Core;

namespace Blazr.Articles.Data;

public class WeatherForecastServerDataBroker : IWeatherForecastDataBroker
{
    private WeatherForecastDataStore _dataStore;

    public WeatherForecastServerDataBroker(WeatherForecastDataStore weatherForecastDataStore)
        => _dataStore = weatherForecastDataStore;

    public ValueTask<List<WeatherForecast>> GetForecastsAsync(ListOptions options)
        => _dataStore.GetForecastsAsync(options);

    public ValueTask<int> GetForecastCountAsync()
        => _dataStore.GetForecastCountAsync();

    public ValueTask<bool> SaveForecastAsync(WeatherForecast record)
        => _dataStore.SaveForecastAsync(record);

    public ValueTask<bool> DeleteForecastAsync(Guid Id)
        => _dataStore.DeleteForecastAsync(Id);
}
```

### WeatherForecastNotificationService

`WeatherForecastNotificationService` is a simple service used by the data services to manage notifications: list updates and page changes.  There are two events triggered by two public notify methods.

```csharp
namespace Blazr.Articles.Core;

public class WeatherForecastNotificationService
{
    public event EventHandler? ListUpdated;
    public event EventHandler? ListPaged;

    public void NotifyListUpdated(object? sender)
        =>  this.ListUpdated?.Invoke(this, EventArgs.Empty);

    public void NotifyListPaged(object? sender, int page)
        =>  this.ListPaged?.Invoke(sender, new PagingEventArgs(page));
}
```

`ListPagedEventArgs` is a derived `Eventargs` class:

```csharp
namespace Blazr.Articles.Core;

public class ListPagedEventArgs : EventArgs
{
    public int Page { get; set; }

    public ListPagedEventArgs(int page)
        => this.Page = page;
}
```

### WeatherForecastListService

This provides list management for the UI. `Records` is the exposed paged record set.  There are two `GetRecordsAsync` public methods:

1. The first is used by the paging control and updates `Records`.
2. The second is used by the `Virtualize` component and implements the `ItemsProviderDelegate` pattern. 

Both update the local `ListOptions` field, get the data set and raise the `ListPaged` event on the notification service.

```csharp
using Microsoft.AspNetCore.Components.Web.Virtualization;
namespace Blazr.Articles.Core;

public class WeatherForecastListService
{
    private IWeatherForecastDataBroker _weatherForecastDataBroker;
    private WeatherForecastNotificationService _notificationService;
    public readonly ListOptions ListOptions = new ListOptions();
    public List<WeatherForecast>? Records { get; private set; }

    public WeatherForecastListService(IWeatherForecastDataBroker weatherForecastDataBroker, WeatherForecastNotificationService weatherForecastNotificationService)
    {
        _weatherForecastDataBroker = weatherForecastDataBroker;
        _notificationService = weatherForecastNotificationService;
    }

    public async ValueTask<ListOptions> GetRecordsAsync(ListOptions options)
    {
        this.ListOptions.Set(options);
        await this.GetRecordsAsync();
        return this.ListOptions.Copy;
    }

    public async ValueTask<ItemsProviderResult<WeatherForecast>> GetRecordsAsync(ItemsProviderRequest request)
    {
        this.ListOptions.Set(request);
        await this.GetRecordsAsync();
        return new ItemsProviderResult<WeatherForecast>(this.Records ?? new List<WeatherForecast>(), this.ListOptions.ListCount);
    }

    private async ValueTask GetRecordsAsync()
    {
        this.Records = await _weatherForecastDataBroker.GetForecastsAsync(this.ListOptions);
        this.ListOptions.ListCount = await _weatherForecastDataBroker.GetForecastCountAsync();
        _notificationService.NotifyListPaged(this, this.ListOptions.Page);
    }
}
```

### WeatherForecastCrudService

`WeatherForecastCrudService` contains the Crud services.

Methods raise the appropriate events on the notification service.  This service is very simple here, but where the record is more complex and/or record editing is implemented, this class would maintain the working copy of the record.

```csharp
namespace Blazr.Articles.Core;

public class WeatherForecastCrudService
{
    private IWeatherForecastDataBroker _weatherForecastDataBroker;
    private WeatherForecastNotificationService _notificationService;

    public WeatherForecastCrudService(IWeatherForecastDataBroker weatherForecastDataBroker, WeatherForecastNotificationService weatherForecastNotificationService)
    {
        _weatherForecastDataBroker = weatherForecastDataBroker;
        _notificationService = weatherForecastNotificationService;
    }

    public async ValueTask DeleteRecordAsync(Guid Id)
    {
        _ = await _weatherForecastDataBroker.DeleteForecastAsync(Id);
        _notificationService.NotifyListUpdated(this);
    }

    public async ValueTask AddRecordAsync(WeatherForecast record)
    {
        _ = await _weatherForecastDataBroker.SaveForecastAsync(record);
        _notificationService.NotifyListUpdated(this);
    }
}
```

This completes the data pipeline.

## The UI

First make a copy of `FetchData`.  Call it `FetchPagedData`, change the `Page` directive to `FetchPagedData` and add a link to the new poage to `NavMenu`.

```
<div class="nav-item px-3">
    <NavLink class="nav-link" href="fetchpageddata">
        <span class="oi oi-list-rich" aria-hidden="true"></span> Paged data
    </NavLink>
</div>
```

### FetchData

We'll look at this in stages.

The page:

1. Implements `IDisposable`: events need disposing correctly.  The Renderer manages `IDisposable`.
2. Injects the View and Notification Services.

```
@page "/fetchdata"
@implements IDisposable
@using Blazr.Articles.Core
@inject WeatherForecastListService ListService
@inject WeatherForecastCrudService CrudService
@inject WeatherForecastNotificationService NotificationService
```

3. Provides a private field to reference the UI `Virtualize` component.
4. `OnListUpdated` handles any list update events.  It calls `RefreshDataAsync` on the `Virtualize` component and `StateHasChanged` on the page component.
5. Implements `Dispose` to unregister event handlers.

```
@code {
    private Virtualize<WeatherForecast>? virtualizeComponent;

    protected override void OnInitialized()
        => this.NotificationService.ListUpdated += this.OnListUpdated;

    private void OnListUpdated(object? sender, EventArgs e)
    {
        this.virtualizeComponent?.RefreshDataAsync();
        this.InvokeAsync(StateHasChanged);
    }

    public void Dispose()
        => this.NotificationService.ListUpdated += this.OnListUpdated;
}
```

6. Add and Delete UI event handlers wired up to the `WeatherForecastCrudService`.

```csharp
    public async Task AddRecord()
    {
        var record = new WeatherForecast
            {
                WeatherForecastId = Guid.NewGuid(),
                Date = DateTime.Now,
                TemperatureC = 20,
                Summary = "Testing"
            };
        await CrudService.AddRecordAsync(record);
    }

    public async Task DeleteRecord(Guid Id)
        => await CrudService.DeleteRecordAsync(Id);
```

On to the UI.

We:
1. Remove the list loading logic: this is handled by the `Virtualize` component.
2. Add a delete button to the row template and a add record button at the top of the page.
3. Add the `Virtualize` component as the row template, wired to the `WeatherForecastListService`method `GetRecordsAsync`.

```html
<h1>Weather forecast</h1>

<p>This component demonstrates fetching data from a service.</p>
<div class="container-fluid">
    <div class="col-12 text-end">
        <button class="btn btn-sm btn-dark" @onclick="this.AddRecord">Add Record</button>
    </div>
</div>
<table class="table">
    <thead>
        <tr>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
            <th>Actions</th>
        </tr>
    </thead>
    <tbody>
        <Virtualize TItem=WeatherForecast Context=forecast ItemsProvider=this.ListService.GetRecordsAsync @ref=this.virtualizeComponent>
            <tr>
                <td>@forecast.Date.ToShortDateString()</td>
                <td>@forecast.TemperatureC</td>
                <td>@forecast.TemperatureF</td>
                <td>@forecast.Summary</td>
                <td>
                    <button class="btn btn-sm btn-danger" @onclick="() => this.DeleteRecord(forecast.WeatherForecastId)">Delete</button>
                </td>
            </tr>
        </Virtualize>
    </tbody>
</table>
```

## Paging

First, a paging component.

### PagingControl

There's a lot of simple maths based fields to calculate and manage the page and block information the display components need.  We'll skip those.

The control follows the same pattern as `Virtualize`:
1. The `ListOptions` class is used to pass and receive paging data.
2. Uses a `Func` delegate, defined as a required Parameter, to call the paging provider.
3. Provides a notification method for the parent to notify that the list has changed. 

The main methods are:

1. `GotToPageAsync(int page)` sets the new page and calls `SetPageAsync`.
2. `GotToPageAsync()` calls `SetPageAsync` with the current `ListOptions` data.
3. `SetPageAsync()` calls the `PagingProvider` and updates the list count on the result.

```csharp
private async Task GotToPageAsync(int page)
{
    if (this.PagingProvider is not null && page != this.Page)
    {
        this.Page = page;
        await GotToPageAsync();
    }
}

private async Task GotToPageAsync()
{
    await SetPageAsync();
    this.StateHasChanged();
}

private async Task SetPageAsync()
{
    if (this.PagingProvider is not null)
    {
        var options = await PagingProvider(_listOptions);
        this.Page = options.Page;
        this.ListCount = options.ListCount;
    }
}
```

`OnInitializedAsync` gets the current page.

```csharp
protected async override Task OnInitializedAsync()
    =>  await this.SetPageAsync();
```

The full partial class looks like this:

```csharp
using Blazr.Articles.Core;
using Microsoft.AspNetCore.Components;

namespace Blazr.Articles.UI;

public partial class PagingControl
    : ComponentBase
{
    private ListOptions _listOptions => new ListOptions() { PageSize = this.PageSize, StartRecord = this.ReadStartRecord };
    private int Page = 0;
    private int ListCount = 0;

    [Parameter] public int PageSize { get; set; } = 5;

    [Parameter] public int BlockSize { get; set; } = 10;

    [Parameter][EditorRequired] public Func<ListOptions, ValueTask<ListOptions>>? PagingProvider { get; set; }

    [Parameter] public bool ShowPageOf { get; set; } = true;

    protected async override Task OnInitializedAsync()
        =>  await this.SetPageAsync();

    private async Task SetPageAsync()
    {
        if (this.PagingProvider is not null)
        {
            var options = await PagingProvider(_listOptions);
            this.Page = options.Page;
            this.ListCount = options.ListCount;
        }
    }

    private void OnPagingReset(object? sender, PagingEventArgs e)
    {
        this.Page = e.Page;
        this.InvokeAsync(StateHasChanged);
    }

    private async Task GotToPageAsync(int page)
    {
        if (this.PagingProvider is not null && page != this.Page)
        {
            this.Page = page;
            await GotToPageAsync();
        }
    }

    private async Task GotToPageAsync()
    {
        await SetPageAsync();
        this.StateHasChanged();
    }

    public async ValueTask NotifyListChangedAsync()
        => await GotToPageAsync();

    private int DisplayPage => this.Page + 1;

    private int LastPage => PageSize == 0 || ListCount == 0
        ? 0
        : ((int)Math.Ceiling(Decimal.Divide(this.ListCount, this.PageSize))) - 1;

    private int LastDisplayPage => this.LastPage + 1;

    private int ReadStartRecord => this.Page * this.PageSize;

    private int Block => (int)Math.Floor(Decimal.Divide(this.Page, this.BlockSize));

    private bool AreBlocks => this.ListCount > this.BlockSize * this.PageSize;

    private int BlockStartPage => this.Block * this.BlockSize;

    private int BlockEndPage => this.LastPage > (this.BlockStartPage + (BlockSize)) - 1
        ? (this.BlockStartPage + BlockSize) - 1
        : this.LastPage;

    private int LastBlock => (int)Math.Floor(Decimal.Divide(this.LastPage, this.BlockSize));

    private int LastBlockStartPage => LastBlock * this.BlockSize;

    private string GetCss(int page)
        => page == this.Page ? "btn-primary" : "btn-secondary";

    private async Task MoveBlockAsync(int block)
    {
        var _page = block switch
        {
            int.MaxValue => this.LastBlockStartPage,
            1 => this.Block + 1 > LastBlock ? LastBlock * this.BlockSize : this.BlockStartPage + BlockSize,
            -1 => this.Block - 1 < 0 ? 0 : this.BlockStartPage - BlockSize,
            _ => 0
        };
        await this.GotToPageAsync(_page);
    }

    private async Task GoToBlockAsync(int block)
        => await this.GotToPageAsync(block * this.PageSize);
}
```

And the Razor markup

```html
@namespace Blazr.Articles.UI
@implements IDisposable

<div class="m-2 p-2">
    @if (this.AreBlocks)
    {
        <div class="btn-group me-1" role="group" aria-label="Move Back Buttons">
            <button type="button" class="btn btn-sm btn-dark" @onclick="() => this.MoveBlockAsync(int.MinValue)">|&lt;</button>
            <button type="button" class="btn btn-sm btn-dark" @onclick="() => this.MoveBlockAsync(-1)">&lt;&lt;</button>
        </div>
    }
    <div class="btn-group" role="group" aria-label="Page Buttons">
        @for (int page = this.BlockStartPage; page <= this.BlockEndPage; page++)
        {
            var pageno = page;
            var viewpageno = page + 1;
            <button type="button" class="btn btn-sm @GetCss(pageno)" @onclick="() => this.GotToPageAsync(pageno)">@viewpageno</button>
        }
    </div>
    @if (this.AreBlocks)
    {
        <div class="btn-group ms-1" role="group" aria-label="Move Forward Buttons">
            <button type="button" class="btn btn-sm btn-dark" @onclick="() => this.MoveBlockAsync(1)">&gt;&gt;</button>
            <button type="button" class="btn btn-sm btn-dark" @onclick="() => this.MoveBlockAsync(int.MaxValue)">&gt;|</button>
        </div>
    }
    @if (this.ShowPageOf)
    {
        <span class="mx-2">Page @this.DisplayPage of @this.LastDisplayPage</span>
    }
</div>
```

## FetchPagedData

We'll look at this in stages.

1. Implements `IDisposable`: events need disposing correctly.  The Renderer manages `IDisposable`.
2. Injects the View and Notification Services.

```
@using Blazr.Articles.Core
@using Blazr.Articles.UI
@inject WeatherForecastListService ListService
@inject WeatherForecastCrudService CrudService
@inject WeatherForecastNotificationService NotificationService
```
3. Provides a private field to reference the UI `PageControl` component.
4. `OnListUpdated` handles any list update events.  It calls `NotifyListChangedAsync` on the `PagingControl` component and `StateHasChanged` on the page component.
5. `OnListPaged` handles any paging events.  It invokes `StateHasChanged` on the page component.
6. Implements `Dispose` to unregister event handlers.

```csharp
private PagingControl? pagingControl;

protected override void OnInitialized()
{
    this.NotificationService.ListUpdated += this.OnListChanged;
    this.NotificationService.ListPaged += this.OnListPaged;
}

private void OnListChanged(object? sender, EventArgs e)
{
    this.pagingControl?.NotifyListChangedAsync();
    this.InvokeAsync(StateHasChanged);
}

private void OnListPaged(object? sender, EventArgs e)
    => this.InvokeAsync(StateHasChanged);

public void Dispose()
{
    this.NotificationService.ListUpdated += this.OnListChanged;
    this.NotificationService.ListPaged += this.OnListPaged;
}
```

7. Add and Delete UI event handlers wired up to the `WeatherForecastCrudService`.

```csharp
public async Task AddRecord()
{
    var record = new WeatherForecast
        {
            WeatherForecastId = Guid.NewGuid(),
            Date = DateTime.Now,
            TemperatureC = 20,
            Summary = "Testing"
        };
    await CrudService.AddRecordAsync(record);
}

public async Task DeleteRecord(Guid Id)
    => await CrudService.DeleteRecordAsync(Id);
```

In the UI markup we:
1. Add the paging control in a top bar along with the add button.
2. Point the list loop to the service `Records` collection.
3. Add the delete button to each row.

```html

<h1>Weather forecast</h1>

<p>This component demonstrates fetching data from a service.</p>

<div class="container-fluid">
    <div class="row">
        <div class="col-10">
            <PagingControl BlockSize=10 PageSize=10 PagingProvider=this.ListService.GetRecordsAsync @ref=this.pagingControl />
        </div>
        <div class="col-2 text-end">
            <button class="btn btn-sm btn-dark" @onclick="this.AddRecord">Add Record</button>
        </div>
    </div>
</div>
@if (this.ListService.Records == null)
{
    <p><em>Loading...</em></p>
}
else
{
    <table class="table">
        <thead>
            <tr>
                <th>Date</th>
                <th>Temp. (C)</th>
                <th>Temp. (F)</th>
                <th>Summary</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var forecast in this.ListService.Records)
            {
                <tr>
                    <td>@forecast.Date.ToShortDateString()</td>
                    <td>@forecast.TemperatureC</td>
                    <td>@forecast.TemperatureF</td>
                    <td>@forecast.Summary</td>
                    <td>
                        <button class="btn btn-sm btn-danger" @onclick="() => this.DeleteRecord(forecast.WeatherForecastId)">Delete</button>
                    </td>
                </tr>
            }
        </tbody>
    </table>
}
```

### How it Works

#### Paging

1. When the page loads, the initial state wil be "loading":  `WeatherForecastListService.Records` will be null. 
2. The `PagingControl` initializes, calling `SetPageAsync` and loading the first page.
3. `WeatherForecastListService.GetRecordsAsync` loads the initial lage of data into `Records` and raises the `NotifyListPaged` event on the notifciation service.
4. `OnListPaged` on `FetchPagedData` is called, which triggers a re-render.  `WeatherForecastListService.Records` now contains a dataset and is rendered.

#### Add/Delete Records.

1. Add/Delete methods on the Crud Service raise the `ListUpdated` event on the notification service.
2. `OnListChanged` is called on `FetchPagedData` which calls `NotifyListChangedAsync` on the `PagingControl`.
3. This triggers a reload of the current page and the `ListChanged` event which re-renders the main page.

## Wrap up

That's it for this article.  We've explored:

1. How to build the data pipeline on a fundimentally sounder foundation.
2. How to separate the data from the UI.
3. How to use the `Virtualize` component, linked into the data pipeline to load paged data.
4. How to build a simple paging control that uses th same paging data pipeline infrastructure.
5. How to use the *Notification* pattern to drive the UI update process.

In a future article I'll look at how to upgrade the data pipeline for sorting and filtering operations, and how to buid a set of generic UI components to handle such operations. 
