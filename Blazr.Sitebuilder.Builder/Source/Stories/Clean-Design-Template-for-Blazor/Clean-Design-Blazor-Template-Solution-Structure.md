
The solution consists of nine projects:

1. *Blazor.Template* is a Razor Library project.  It contains the application base code.
2. *Blazor.Template.Data* is a Razor Library project.  It contains all the Data Domain shared code.
3. *Blazor.Template.Core* is a Razor Library project.  It contains all the Core Domain shared code.
4. *Blazor.Template.UI* is a Razor Library project.  It contains all the UI Domain shared code.
5. *Blazor.Template.APIControllers* is a Razor Library project.  It contains all the API Controllers code.
6. *Blazor.Template.WASM* is a `BlazorWebAssembly` project.  It builds the WASM compliant code to deploy to the browser.
7. *Blazor.Template.Server.Web* is an `AspNetCore` Web project, configured to support the Blazor Server Hub, and contains the launch file for the Blazor Server SPA.
8. *Blazor.Template.WASM.Web* is an `AspNetCore` Web project configured to provide server side files for the WASM SPA, the launch file for the Blazor Server SPA, and any API conbntrollers.
9. *Blazor.Template.Tests* is a XUnit Test project.  It contains all the test code for the solution.

## Running the Solution.

The solution can be run with either Web projct as the startup project:

 - *Blazor.Template.Server.Web* runs the solution in Server mode.
 - *Blazor.Template.WASM.Web* runs the project in WASM mode.

## Structure and Namespaces

All the primary application code is split into three *domains*.  These are:

1. *Data Domain* - `Blazr.Template.Data` - Data access code.  The only application dependancy is to the `Blazr.Template.Core`.
2. *Core Domain* - `Blazr.Template.Core` - Application and Business Logic code.  Core code has no dependancies on other application domain code.
3. *UI Domain* - `Blazr.Template.UI` - UI code, principally components and supporting classes .  UI code only depends on `Blazr.Template.Core`.  There is no dependancies on `Blazr.Template.Data`.

![Domain model](https://shauncurtis.github.io/Design/assets/Domain-Model.png)

The documents associated with this demo ate:

 - [Solution Structure](/Clean-Design-Blazor-Template-Solution-Structure.html)
 - [Data Services](./Clean-Design-Blazor-Template-DataServices.html)
 - [UI](./Clean-Design-Blazor-Template-UI.html)
 - [Solution Setup](./Clean-Design-Blazor-Template-Solution-Setup.html)
 - [Testing](./Clean-Design-Blazor-Template-Solution-Testing.html)
