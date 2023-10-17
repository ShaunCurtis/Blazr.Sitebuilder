
## Data Classes

The data classes are:

### PagingData

Project: *Blazr.Template.Core*.

PagingData is used to:

1. Pass paging information to the data brokers to retrieve paged data from the data source.
2. Control the UI Paging Control behaviour.

### WeatherForecast

Project: *Blazr.Template.Core*.
This is the original with the following changes:

1. Defined as a `record` with imutable properties.
2. `Id` field defined as a `Guid`.

## Data Services

The data pathway consists of three services.

1. WeatherForecastDataStore
2. IWeatherForecastDataBroker
3. WeatherForecastViewService

Providing the following methods:

1. `GetRecordsAsync`  - gets the recordset.
2. `GetRecordCountAsync` - gets the record count.
3. `GetPagedRecordsAsync` - gets a paged recordset.

All functionality is implemented using async patterns.  Some demo code may be synchronous code in a `Task` wrapper: real database/API functionality will be async. 

### WeatherForecastDataStore

Project: *Blazr.Template.Data*.

The `WeatherForecastDataStore` class emulates a database.  It's a singleton service, and on initialization builds a data set thst it then copies in responsing to data requests. The three principal public methods are shown below.  Note the use of `ValueTask` instead of `Task`.

```csharp
public ValueTask<List<WeatherForecast>> GetRecordsAsync()
    => ValueTask.FromResult(GetListCopy(_records));

public ValueTask<int> GetRecordCountAsync()
    => ValueTask.FromResult(_records.Count);

public ValueTask<List<WeatherForecast>> GetPagedRecordsAsync(PagingData pagingData)
{
    pagingData.SetRecordCount( _records.Count);
    var list = _records.Skip(pagingData.ReadStartRecord).Take(pagingData.PageSize).ToList();
    return ValueTask.FromResult(GetListCopy(list));
}
```

### IWeatherForecastDataBroker

Project: *Blazr.Template.Core*.

`IWeatherForecastDataBroker` defines the interface for the core domain to data domain data transfer.  Using an interface in DI decouples out two domains.

```csharp
public ValueTask<List<WeatherForecast>> GetRecordsAsync();
public ValueTask<int> GetRecordCountAsync();
public ValueTask<List<WeatherForecast>> GetPagedRecordsAsync(int page);
```
There are two concrete implementations:

#### WeatherForecastServerDataBroker

Project: *Blazr.Template.Data*.

The server version, used by Blazor Server and the API server for Blazor WASM.  It initializes with the singleton `WeatherForecastDataStore` DI instance, and accesses the `WeatherForecastDataStore` for it's data.  `GetPagedRecordsAsync` is shown below.

```csharp
public ValueTask<List<WeatherForecast>> GetPagedRecordsAsync(PagingData pagingData)
    => _dataStore.GetPagedRecordsAsync(pagingData);
```

#### WeatherForecastAPIDataBroker

Project: *Blazr.Template.Data*.

The WASM version.  It makes API calls through an `HttpClient` instance it gets through DI.  `GetPagedRecordsAsync` is shown below.

```csharp
public async ValueTask<List<WeatherForecast>> GetPagedRecordsAsync(PagingData pagingData)
{
    var response = await this.HttpClient.PostAsJsonAsync($"/api/weatherforecast/GetPagedRecordsAsync", pagingData);
    return await response.Content.ReadFromJsonAsync<List<WeatherForecast>>();
}
```

## API Controllers

Project: *Blazr.Template.APIControllers*.

The controllers are separated into the *Blazr.Template.Controllers* project.

`WeatherForecastController` is a bulk standard controller.  `GetPagedRecordsAsync` is shown below.

```csharp
[Route("/api/weatherforecast/listpaged")]
[HttpPost]
public async Task<List<WeatherForecast>> GetPagedRecordsAsync([FromBody] PagingData pagingData) => await _dataBroker.GetPagedRecordsAsync(pagingData);
```

## View Services

Project: *Blazr.Template.Core*.

### WeatherForecastViewService

`WeatherForecastViewService` provides the data and state services for WeatherForecasts to the UI.  It's `Scoped`, one instance per SPA session.

It provides some important functions:

1. Maintains one version of the truth - the WeatherForecast data set.  Components that use the service reference the record list directly, they don't keep copies.
2. Provides alerts, though Events, when the recordset changes.
3. Maintains the page being displayed within the SPA instance through page changes.
4. Uses an interface to inject and interact with the data broker.

```csharp
public class WeatherForecastViewService
{
    private readonly IWeatherForecastDataBroker _dataBroker;
    public PagingData PagingData { get; init; } = new PagingData(0,10,0);
    public bool IsLoaded => Records is not null;
    public List<WeatherForecast>? Records { get; private set; } = null;
    public event EventHandler? ListChanged;

    public WeatherForecastViewService(IWeatherForecastDataBroker weatherForecastDataBroker)
    { 
        _dataBroker = weatherForecastDataBroker;
        this.PagingData.PagingChanged += this.OnPageChanged;
    }

    public async Task<bool> GetRecordsAsync()
    {
        this.Records = await _dataBroker.GetRecordsAsync();
        this.NotifyRecordChanged();
        return true;
    }

    public async Task<bool> GetRecordsAsync(int page)
    {
        var recordCount = await _dataBroker.GetRecordCountAsync();
        page = page == int.MinValue ? this.PagingData.Page : page;
        this.PagingData.Set(page, recordCount);
        this.Records = await _dataBroker.GetPagedRecordsAsync(this.PagingData);
        this.NotifyRecordChanged();
        return true;
    }

    public async Task<bool> GetPagedRecordsAsync()
    {
        this.Records = await _dataBroker.GetPagedRecordsAsync(this.PagingData);
        this.PagingData.SetRecordCountSilently(await _dataBroker.GetRecordCountAsync());
        this.NotifyRecordChanged();
        return true;
    }

    public void NotifyRecordChanged(object? sender = null)
    {
        sender ??= this;
        this.ListChanged?.Invoke(sender, EventArgs.Empty);
    }

    private async void OnPageChanged(object? sender, EventArgs e)
        => await this.GetPagedRecordsAsync();
}
```

## Wrap up

The code here is not Blazor.  It's standard dotNetCore, building classes and using the dotNetCore web server DI services.
