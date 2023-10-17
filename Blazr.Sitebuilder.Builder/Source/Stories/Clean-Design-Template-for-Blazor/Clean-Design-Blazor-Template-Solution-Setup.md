
## Service Configurations

Project: *Blazr.Template*.

Service configurations are set up in a `ServiceCollection` extension class.

There's a helper method to add the common services.

```csharp
private static void AddCommonServices(this IServiceCollection services)
{
    services.AddSingleton<WeatherForecastDataStore>();
    services.AddScoped<WeatherForecastViewService>();
}
```

The Server configuration adds the server specific data broker `WeatherForecastServerDataBroker`.

```csharp
public static IServiceCollection AddServerApplicationServices(this IServiceCollection services)
{
    services.AddSingleton<IWeatherForecastDataBroker, WeatherForecastServerDataBroker>();
    AddCommonServices(services);
    return services;
}
```

The WASM client configuration adds the WASM specific data broker `WeatherForecastAPIDataBroker`.

```csharp
public static IServiceCollection AddWASMApplicationServices(this IServiceCollection services)
{
    services.AddScoped<IWeatherForecastDataBroker, WeatherForecastAPIDataBroker>();
    AddCommonServices(services);
    return services;
}
```

The final configuration is for the WASM (API) Server.  It adds the server data services and the controllers in the specified assembly.

```cshsrp
public static IServiceCollection AddWASMServerApplicationServices(this IServiceCollection services)
{
    services.AddSingleton<WeatherForecastDataStore>();
    services.AddSingleton<IWeatherForecastDataBroker, WeatherForecastServerDataBroker>();
    return services;
}
```

## Blazr.Template.Server.Web

The Blazor Server web site.

![Blazr.Temlate.Server.Web File Structure](https://shauncurtis.github.io/Design/assets/Blazr.Template.Server.Web-File-Structure.png)

### Project File

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Blazr.Template.Core\Blazr.Template.Core.csproj" />
    <ProjectReference Include="..\Blazr.Template.Data\Blazr.Template.Data.csproj" />
    <ProjectReference Include="..\Blazr.Template.UI\Blazr.Template.UI.csproj" />
    <ProjectReference Include="..\Blazr.Template\Blazr.Template.csproj" />
  </ItemGroup>

</Project>
```

### Startup

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddRazorPages();
    services.AddServerSideBlazor();
    services.AddServerApplicationServices();
}
```

### _Host.cshtml

The standard Blazor server file, with a modified component definition providing the fully qualified source for `App`.

```csharp
    <component type="typeof(Blazr.Template.UI.App)" render-mode="ServerPrerendered" />
```

### wwwroot

Thhe standard Blazor server files.

## Blazr.Template.WASM

The project that builds the WASM specific compiled code.

![Blazr.Temlate.WASM File Structure](https://shauncurtis.github.io/Design/assets/Blazr.Template.WASM-File-Structure.png)

### Project File

```xml
<Project Sdk="Microsoft.NET.Sdk.BlazorWebAssembly">

  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly" Version="5.0.12" />
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.DevServer" Version="5.0.12" PrivateAssets="all" />
    <PackageReference Include="System.Net.Http.Json" Version="5.0.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Blazr.Template.Core\Blazr.Template.Core.csproj" />
    <ProjectReference Include="..\Blazr.Template.Data\Blazr.Template.Data.csproj" />
    <ProjectReference Include="..\Blazr.Template.UI\Blazr.Template.UI.csproj" />
    <ProjectReference Include="..\Blazr.Template\Blazr.Template.csproj" />
  </ItemGroup>

</Project>
```

### Program

1. Sets the root component to `Blazr.Template.UI.App`. 
2. Adds the WASM services through the extension method.

```csharp
public static async Task Main(string[] args)
{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    var services = builder.Services;
    builder.RootComponents.Add<Blazr.Template.UI.App>("#app");

    services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    services.AddWASMApplicationServices();

    await builder.Build().RunAsync();
}
```

### wwwroot

The standard WASM project wwwroot files.

## Blazr.Template.WASM.Web

The Blazor WASM SPA host site with the API controllers.

![Blazr.Temlate.WASM.Web File Structure](https://shauncurtis.github.io/Design/assets/Blazr.Template.WASM.Web-File-Structure.png)

### Project File

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.Server" Version="5.0.12" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Blazr.Template.Core\Blazr.Template.Core.csproj" />
    <ProjectReference Include="..\Blazr.Template.Data\Blazr.Template.Data.csproj" />
    <ProjectReference Include="..\Blazr.Template.UI\Blazr.Template.UI.csproj" />
    <ProjectReference Include="..\Blazr.Template.WASM\Blazr.Template.WASM.csproj" />
    <ProjectReference Include="..\Blazr.Template\Blazr.Template.csproj" />
  </ItemGroup>

</Project>
```

### StartUp

```csharp
services.AddRazorPages();
services.AddWASMServerApplicationServices();
services.AddControllers().PartManager.ApplicationParts.Add(new AssemblyPart(typeof(Blazr.Template.APIControllers.WeatherForecastController).Assembly));
```

## Project Files

#### Blazr.Template.Core Project File

```xml
<Project Sdk="Microsoft.NET.Sdk.Razor">
  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
	  <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <SupportedPlatform Include="browser" />
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.Web" Version="5.0.12" />
  </ItemGroup>
</Project>
```

#### Blazr.Template.Data Project File

```xml
<Project Sdk="Microsoft.NET.Sdk.Razor">
  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
	  <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.Web" Version="5.0.12" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\Blazr.Template.Core\Blazr.Template.Core.csproj" />
  </ItemGroup>
</Project>
```

#### Blazr.Template.UI Project File

```xml
<Project Sdk="Microsoft.NET.Sdk.Razor">
  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
	  <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.Web" Version="5.0.12" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\Blazr.Template.Core\Blazr.Template.Core.csproj" />
  </ItemGroup>
</Project>
```

#### Blazr.Template Project File

```xml
<Project Sdk="Microsoft.NET.Sdk.Razor">
  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
	  <Nullable>enable</Nullable>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.Web" Version="5.0.12" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\Blazr.Template.Core\Blazr.Template.Core.csproj" />
    <ProjectReference Include="..\Blazr.Template.Data\Blazr.Template.Data.csproj" />
  </ItemGroup>
</Project>
```


