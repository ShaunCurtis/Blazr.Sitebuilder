
This is the documentation set to accompany the Blazor Clean Design Template GitHub Repositories.

There are two repositories:

 - [Blazor.Demo](https://github.com/ShaunCurtis/Blazr.Demo) contains the buildable code from which the template is extracted.
 - [Blazor.Demo.Template](https://github.com/ShaunCurtis/Blazr.Demo.Template) contains the template configuration and Template Zip file.

## Project Documents

 - [Solution Structure](./stories/Clean-Design-Template-for-Blazor/Clean-Design-Blazor-Template-Solution-Structure.html)
 - [Data Services](./stories/Clean-Design-Template-for-Blazor/Clean-Design-Blazor-Template-DataServices.html)
 - [UI](./stories/Clean-Design-Template-for-Blazor/Clean-Design-Blazor-Template-UI.html)
 - [Solution Setup](./stories/Clean-Design-Template-for-Blazor/Clean-Design-Blazor-Template-Solution-Setup.html)
 - [Testing](./stories/Clean-Design-Template-for-Blazor/Clean-Design-Blazor-Template-Testing.html)

The template demonstrates:

1. A Structured Data/UI design using *Separation Of Concern* principles.
2. Using Components to structure the UI and automate common UI patterns.
3. Building solutions that can be run in Server or WASM modes from the same common code base.

It uses the three domain model to structure the solution.

![Domain model](https://shauncurtis.github.io/Design/assets/Domain-Model.png)

The structure is enforced through projects.  Each domain has a project, with project dependencies enforcing code dependencies:

1. Core project has no project dependencies.
2. Data project only depends on the Core project.
3. UI project only depends on the Core project.

The solution consists of nine projects.  This may at first seem overkill, but it organises your code logically. The projects enforce the design dependencies and separation of concern principles.

1. *Blazor.Template* is a Razor Library project.  It contains the application base code.
2. *Blazor.Template.Data* is a Razor Library project.  It contains all the Data Domain shared code.
3. *Blazor.Template.Core* is a Razor Library project.  It contains all the Core Domain shared code.
4. *Blazor.Template.UI* is a Razor Library project.  It contains all the UI Domain shared code.
5. *Blazor.Template.APIControllers* is a Razor Library project.  It contains all the API Controllers code.
6. *Blazor.Template.WASM* is a `BlazorWebAssembly` project.  It builds the WASM compliant code to deploy to the browser.
7. *Blazor.Template.Server.Web* is an `AspNetCore` Web project, configured to support the Blazor Server Hub, and contains the launch file for the Blazor Server SPA.
8. *Blazor.Template.WASM.Web* is an `AspNetCore` Web project configured to provide server side files for the WASM SPA, the launch file for the Blazor Server SPA, and any API conbntrollers.
9. *Blazor.Template.Tests* is a XUnit Test project.  It contains all the test code for the solution.

### Running the Solution.

The solution can be run with either Web project as the startup project:

 - *Blazor.Template.Server.Web* runs the solution in Server mode.
 - *Blazor.Template.WASM.Web* runs the project in WASM mode.
 
