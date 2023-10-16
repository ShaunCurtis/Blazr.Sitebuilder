using Blazr.Sitebuilder.Builder;
using Blazr.Sitebuilder.Builder.Source;
using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<HtmlRenderer>();
builder.Services.AddScoped<ContentRenderer>();
builder.Services.AddSingleton<RouteFactory>();
builder.Services.AddSingleton<StoryFactory>();
builder.Services.AddSingleton<SiteBuilderFactory>();


var app = builder.Build();

//// Inject the renderer and optionally a name from the querystring
//app.MapGet("/", async (ContentRenderer renderer, string name = "world") =>
//{
//    // Pass the parameters and render the component
//    //var html = await renderer.RenderComponent<App>(new() { { nameof(App.RouteComponent), typeof(Blazor_Components) } });
//    var html = await renderer.RenderComponent<Blazor_Components>();

//    // Return the result as HTML
//    return Results.Content(html, "text/html");
//});

var siteBuilderFactory = app.Services.GetRequiredService<SiteBuilderFactory>();

if (siteBuilderFactory is not null)
    await siteBuilderFactory.BuildSiteAsync();

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();
