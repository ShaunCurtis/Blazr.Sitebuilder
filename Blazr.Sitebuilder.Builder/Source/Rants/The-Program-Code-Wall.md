
We've all stared at a wall of code and thought "Blimey what does this do, where do I start?"

How many times have you repeated the same DI setup code in different places?

The solution is to use `IServiceCollection` and `WebApplicationBuilder` extension methods.

Here's an example where I declare `AddAppServerInfrastructureServices` as an extension method on `IServiceCollection`.

It adds all the infrastructure project services and calls individual entity/feature extension methods to add any specific services.  Note I can define different methods to add services for different application deployments: in this case Server and WASM.

```csharp
public static class ApplicationInfrastructureServices
{
    public static void AddAppServerInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IDataBroker, ServerDataBroker>();

        // Add the standard handlers
        services.AddScoped<IListRequestHandler, ListRequestServerHandler<InMemoryTestDbContext>>();
        services.AddScoped<IItemRequestHandler, ItemRequestServerHandler<InMemoryTestDbContext>>();
        services.AddScoped<ICommandHandler, CommandServerHandler<InMemoryTestDbContext>>();

        // Add any individual entity services
        services.AddWeatherForecastServerInfrastructureServices();
    }

    public static void AddAppWASMInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IDataBroker, APIDataBroker>();

        //...
    }
}
```

I then just call the extension method in `Program`.  

A overview of what's configured, not the mind boggling chaining detail.  I can drill down into the detail, but it's not in our face every time I look at `Program`.

```csharp
var builder = WebApplication.CreateBuilder(args);

//...

builder.Services.AddAppServerInfrastructureServices();
builder.Services.AddAppServerPresentationServices();
builder.Services.AddAppServerUIServices();

var app = builder.Build();

//...

app.Run();

```

I access the configuration information I create an extension method for the `WebApplicationBuilder` like this:

```csharp
public static class ApplicationInfrastructureServices
{
    public static void AddAppServerInfrastructureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        var dbConnectionString = configuration.GetValue<string>("DevelopmentConfiguration:DBContext");

        services.AddDbContextFactory<XXXDbContext>(options => options.UseSqlServer(dbConnectionString));

        services.AddScoped<IDataBroker, ServerDataBroker>();

        //...

    }
}
```