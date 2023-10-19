
You've built the out-of-the-box template and played around a little with the code.  You're now ready to start your first application.

It's a little daunting to know where to start?  You expect to make mistakes, take a few wrong turnings, ....

This primer is intended to provide guidance on how to get up and running.  It's neither a set of best practices (*..I hate that phrase*), nor highly opinionated.  Take it or leave it, use as little or as much as you want.  It's aimed primarily at the small to medium sized project with one or two developers working on it.  It takes a very practical approach: starts with the standard Blazor Server template and turns it into a properly structured and testable solution.

## Methodology

Pick your methodology, there are plenty out there.  Mine is a fairly simple three domain model shown below.

![Methodologies](/siteimages/articles/DB-Primer/Methodology.png)

So what are we trying to achieve with a methodology?

It's your application, you can build it whatever way you wish.  You know it intimately, so when things go wrong you know where to look.  **But**:

1. What happens if you walk away?
2. What if someone else has to maintain it?  
3. What happens when you come back to it a year later?  
4. How do you test new functionality and updates?  
5. How do you change to a different data source?  
6. How do you do a major frontend update? 

To answer at least some of these questions, you need to apply structure and principles to you code base.

1.  **Separation of Concerns**.  Classes should represent a single *unit of work*.  UI code should not include database access or business logic functionality.  Building a display table and fetching the data are two separate units of work.
2.  **Inversion of Control**.  Higher level classes should not depend on lower level classes.  A business logic class should not contain specific declarations for database access classes.
3.  **Enforce Project Dependancy**.  Use code projects with strict dependancies to provide separation of concerns.  In my framework there are separate projects for the UI, Core and Data domains, with project level defined project to project dependancies.  I can't code a business logic class with a dependancy on a Data Domain class.

Your real application is the **Core Domain**: Core = Application and Business logic code.  It represents what makes your application unique.  You should be able to change out the data source and the UI with no impact on the core domain code.  Core domain code only depends on Blazor and third party libraries: there must be no dependancies on the other application domains.  The *Data Domain* provides the interface into the data storage.  The *UI Domain* contains all the UI code.  Both depend on the *Core Domain* but not each other.

Core to Data Domain communications are implemented through interfaces.  Our core domain classes make data requests through an `IDataConnector` interface.  This all plugs in the Blazor Services container.  

To understand this, our application services definition will look like:

```csharp
    // Data Domain Code
    services.AddSingleton<WeatherDataStore>();
    services.AddSingleton<IDataBroker, ServerDataBroker>();
    // Core Domain code
    services.AddScoped<IDataConnector, DataConnector>();
    services.AddScoped<WeatherForecastViewService>();
```

Our business logic class `WeatherForecastViewService`'s constructor has an `IDataConnector` argument.  When the Services Container instantiates `WeatherForecastViewService` it injects its instance of `IDataConnector`.  In the code above this is `DataConnector`.   `DataConnector`'s constructor defines `IDataBroker` argument.  This is `ServerDataBroker` which had a constructor argument `WeatherDataStore`, ....

The benefits become apparent when:
 - We need to change out the Data Store.  We'll see this later when we implement a SQL database data store.
 - When we set up the WASM SPA.  It uses a `APIDataBroker` to access data.  THe services definition is shown below.  The core domain services don't change, just the defined `IDataBroker`.

```csharp
    // Data Domain Code
    services.AddScoped<IDataBroker, APIDataBroker>();
    // Core Domain code
    services.AddScoped<IDataConnector, DataConnector>();
    services.AddScoped<WeatherForecastViewService>();
```

## The Initial Solution

Create a new solution using the standard *Blazor Server App* template with no authenication.

 - Solution Name: **Blazr.Primer**.
 - Project Name: **Blazr.Primer.Web**.

Run the solution to make sure it builds and runs.

Why use Server if I want a WebAssembly application?

Simple.  It's faster, easier and more efficient developing with Server.  Design your application correctly, and it will run in either mode.  In Chapter 5 you will learn how to modify the solution to dual mode operation, running both the Server and WASM SPAs from the same web site.

You should now have a solution with one "Web" project.

Create the following projects in the solution:
1. *Blazr.Primer.Core* using the *Class Library* template.
2. *Blazr.Primer.Data* using the *Class Library* template.
3. *Blazr.Primer.UI* using the *Razor Class Library* template.
4. *Blazr.Primer.Test* using the *xUnit Test Project* template.
5. *Blazr.Primer* using the *Razor Class Library* template.

### Blazr.Primer

Clear everything. and add the following folders:

- /Extensions

Update the Project file.

```xml
<Project Sdk="Microsoft.NET.Sdk.Razor">

  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.Web" Version="5.0.9" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Blazr.Primer.Core\Blazr.Primer.Core.csproj" />
    <ProjectReference Include="..\Blazr.Primer.Data\Blazr.Primer.Data.csproj" />
  </ItemGroup>

  <ItemGroup>
    <Folder Include="Extensions\" />
  </ItemGroup>

</Project>
```

### Blazr.Primer.Core

Clear everything add the following folders:

- /Connectors
- /DataClasses
- /Data
- /Interfaces
- /ViewServices

Update the Project file.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <Folder Include="Connectors\" />
    <Folder Include="DataClasses\" />
    <Folder Include="Data\" />
    <Folder Include="Interfaces\" />
    <Folder Include="ViewServices\" />
  </ItemGroup>

</Project>
```

### Blazr.Primer.Data

Clear everything and add the following folders:

- /Brokers
- /DB

Update the Project file.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Blazr.Primer.Core\Blazr.Primer.Core.csproj" />
  </ItemGroup>

  <ItemGroup>
    <Folder Include="Brokers\" />
    <Folder Include="DB\" />
  </ItemGroup>

</Project>
```

### Blazr.Primer.Test

Clear everything and add the following folders:

- /Base
- /Components
- /System
- /Unit

Update the Project file.

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>

    <IsPackable>false</IsPackable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="bunit" Version="1.2.49" />
    <PackageReference Include="bunit.web.testcomponents" Version="1.2.49" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.11.0" />
    <PackageReference Include="xunit" Version="2.4.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.3">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="coverlet.collector" Version="3.1.0">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="FluentAssertions" Version="6.0.0" />
    <PackageReference Include="Tynamix.ObjectFiller" Version="1.5.6" />
    <PackageReference Include="Moq" Version="4.16.1" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Blazr.Primer.Core\Blazr.Primer.Core.csproj" />
    <ProjectReference Include="..\Blazr.Primer.Data\Blazr.Primer.Data.csproj" />
    <ProjectReference Include="..\Blazr.Primer.UI\Blazr.Primer.UI.csproj" />
  </ItemGroup>

<ItemGroup>
    <Folder Include="Base\" />
    <Folder Include="Components\" />
    <Folder Include="System\" />
    <Folder Include="Unit\" />
  </ItemGroup>

</Project>
```

### Blazr.Primer.UI

Clear everything and add the following folders:

 - /Components/AppControls
 - /Components/Base
 - /Components/FormControls
 - /Components/ListControls
 - /Extensions/
 - /Forms/
 - /Helpers/
 - /RouteViews/

Add `@using Blazr.Primer.UI.Components` to *_Imports.razor*

Update the Project file.

```xml
<Project Sdk="Microsoft.NET.Sdk.Razor">

  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.Web" Version="5.0.9" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Blazr.Primer.Core\Blazr.Primer.Core.csproj" />
  </ItemGroup>

  <ItemGroup>
    <Folder Include="Components\AppControls\" />
    <Folder Include="Components\Base\" />
    <Folder Include="Components\FormControls\" />
    <Folder Include="Components\ListControls\" />
    <Folder Include="Extensions\" />
    <Folder Include="Forms\" />
    <Folder Include="Helpers\" />
    <Folder Include="RouteViews\" />
  </ItemGroup>

</Project>
```

### Blazr.Primer.Web

1. Update *_Imports.razor*

```csharp
@using System.Net.Http
@using Microsoft.AspNetCore.Authorization
@using Microsoft.AspNetCore.Components.Forms
@using Microsoft.AspNetCore.Components.Routing
@using Microsoft.AspNetCore.Components.Web
@using Microsoft.AspNetCore.Components.Web.Virtualization
@using Microsoft.JSInterop
```
2. Move the contents of *Shared* to *Blazr.Primer.UI/Components/AppControls*, and delete *Shared*.

3. Move *App.razor to *Blazr.Primer.UI/Components/AppControls*.

3. Add `@namespace Blazr.Primer.UI.Components` as the top line of the four moved files *NavMenu.razor*, *MainLayout.razor*, *SurveyPrompt.razor* and *App.razor*.

4. Update *App.razor*.

```xml
@namespace Blazr.Primer.UI.Components
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

5. Move *Index.Razor*, *FetchData.razor* and *Counter.razor* to *Blazr.Primer.UI/RouteViews*.

6. Add `@namespace Blazr.Primer.UI.RouteViews` as the second line in each of the moved files.

7. Update *FetchData.razor*

```html
@page "/fetchdata"
@using Blazr.Primer.Core
@namespace Blazr.Primer.UI.RouteViews
@inject WeatherForecastService ForecastService

<h1>Weather forecast</h1>
.....
```

8. Move *Data/WeatherForecast.cs* to *Blazr.Primer.Core/DataClasses*, and change it's namespace to `namespace Blazr.Primer.Core`.

9.  Move *Data/WeatherForecastService.cs* to *Blazr.Primer.Core/ViewServices*, and change it's namespace to `namespace Blazr.Primer.Core`.

10. Delete *Data* folder.

11. Update the project file.

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.Server" Version="5.0.9" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Blazr.Primer.Core\Blazr.Primer.Core.csproj" />
    <ProjectReference Include="..\Blazr.Primer.Data\Blazr.Primer.Data.csproj" />
    <ProjectReference Include="..\Blazr.Primer\Blazr.Primer.csproj" />
    <ProjectReference Include="..\Blazr.Primer.UI\Blazr.Primer.UI.csproj" />
  </ItemGroup>

</Project>
```

11. Update *_Import.razor*

```csharp
@using System.Net.Http
@using Microsoft.AspNetCore.Authorization
@using Microsoft.AspNetCore.Components.Authorization
@using Microsoft.AspNetCore.Components.Forms
@using Microsoft.AspNetCore.Components.Routing
@using Microsoft.AspNetCore.Components.Web
@using Microsoft.AspNetCore.Components.Web.Virtualization
@using Microsoft.JSInterop
@using Blazr.Primer.Web
```

12. Update *_host.cshtml*

```html
@page "/"
@namespace Blazr.Primer.Web.Pages
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@{
    Layout = null;
}

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Blazr.Primer.Web</title>
    <base href="~/" />
    <link rel="stylesheet" href="/css/bootstrap/bootstrap.min.css" />
    <link href="/css/site.css" rel="stylesheet" />
    <link href="/Blazr.Primer.Web.styles.css" rel="stylesheet" />
</head>
<body>
    <component type="typeof(Blazr.Primer.UI.Components.App)" render-mode="ServerPrerendered" />

    <div id="blazor-error-ui">
        <environment include="Staging,Production">
            An error has occurred. This application may no longer respond until reloaded.
        </environment>
        <environment include="Development">
            An unhandled exception has occurred. See browser dev tools for details.
        </environment>
        <a href="" class="reload">Reload</a>
        <a class="dismiss">🗙</a>
    </div>

    <script src="_framework/blazor.server.js"></script>
</body>
</html>
```

You should now have a solution that looks like:

![Solution](/siteimages/Articles/DB-Primer/Chapter-1-Solution.png)

With dependancies as shown.  Ignore *Blazr.Primer.Controller* and *Blazr.Primer.WASM*.  We will add these later.

![Project Dependancies](/siteimages/Articles/DB-Primer/Project-Dependancies.png)

##  Testing

Testing and coding should go side by side.  I'm not an advocate of Test Driven Design [TTD] but I do believe in developing and testing at the same time.  Write some code, write a test to make sure it does what you think it does, and what what happens in edge conditions.   There's no separate Testing sections in this book, tests are build as services and components are built.

In general, we write one of six classes types:

1. **Infrastructure Classes** - `Startup`, `Program`.
2. **Helper Classes** - utility or helper classes for our main code.  These can include extension classes.
3. **Data Classes** - classes that represent data we pass around our applications - `WeatherForecast` for example.
4. **Service Classes** - classes that will run in the Services Container and are available for injection.
5. **Controllers** - API classes that run on a standard ASP.Net Core web server.
6. **Components** - UI components.  `Index.razor`, `NavMenu.razor`, `App.razor`.  We write these in Razor, but the get compiled into classes of the same name.

The majority of code will be services and components.  These are also the hardest to test.  In the application we will concentrate on these.

Our test frameworks will:

1. Use **xUnit** as the main framework.
2. Use **bUnit** for Renderer and RenderTree emulation in component testing.
3. Not use Selenium.  
4. Use **Moq** extensively to mock up services and test service interactions.
5. Use **FluentAssertions** to expanding our `Assert` statements.
6. Use **Tynamix.ObjectFiller** to generate data in some classes.