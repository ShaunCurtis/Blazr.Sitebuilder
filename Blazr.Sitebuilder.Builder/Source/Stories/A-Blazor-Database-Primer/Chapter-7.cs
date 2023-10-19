using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/Stories/A-Blazor-Database-Primer/Chapter-7")]
public class A_Blazor_Database_Primer_Chapter7 : StoryBase
{
    public override PageData PageData { get; } = new()
    {
        Title = "Chapter 7 - Adding Sorting and Paging to the List Form",
        PublishDate = new DateOnly(2021, 8, 13),
        LastUpdated = new(2021, 8, 13),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story= "A Blazor Database Primer",
        Order=70,
        Tags = "#Blazor;#Database;",
        Description = "In this chapter we will add sorting and paging to the WeatherForecast list form.",
        ContentDirectory = $@".\Source\Stories\A-Blazor-Database-Primer\",
        MarkdownFile = "Chapter-7.md",
    };
}
