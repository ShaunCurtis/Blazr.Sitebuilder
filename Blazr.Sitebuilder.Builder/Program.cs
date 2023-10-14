using Blazr.Sitebuilder.Builder.Code;
using Blazr.Sitebuilder.Builder.Source;
using Blazr.Sitebuilder.Builder.Templates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<HtmlRenderer>();
builder.Services.AddScoped<ContentRenderer>();

var app = builder.Build();

// Inject the renderer and optionally a name from the querystring
app.MapGet("/", async (ContentRenderer renderer, string name = "world") =>
{
    // Pass the parameters and render the component
    var html = await renderer.RenderComponent<App>(new() { { nameof(App.RouteComponent), typeof(Article) } });

    // Return the result as HTML
    return Results.Content(html, "text/html");
});

app.UseStaticFiles();

app.Run();

// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
