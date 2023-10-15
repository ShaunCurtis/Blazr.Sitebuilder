using Blazr.Sitebuilder.Builder;
using Blazr.Sitebuilder.Builder.Source;
using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<HtmlRenderer>();
builder.Services.AddScoped<ContentRenderer>();

var app = builder.Build();

// Inject the renderer and optionally a name from the querystring
app.MapGet("/", async (ContentRenderer renderer, string name = "world") =>
{
    // Pass the parameters and render the component
    //var html = await renderer.RenderComponent<App>(new() { { nameof(App.RouteComponent), typeof(Blazor_Components) } });
    var html = await renderer.RenderComponent<Blazor_Components>();

    // Return the result as HTML
    return Results.Content(html, "text/html");
});

app.UseStaticFiles();

app.Run();

// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
