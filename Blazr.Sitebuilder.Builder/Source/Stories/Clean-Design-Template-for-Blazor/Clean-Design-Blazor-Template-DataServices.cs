﻿using Blazr.Sitebuilder.Builder.Templates;
using Blazr.SiteBuilder;

namespace Blazr.Sitebuilder.Builder;

[Route("/stories/Clean-Design-Template-for-Blazor/Clean-Design-Blazor-Template-DataServices")]
public class Clean_Design_Blazor_Template_DataServices : ArticleBase
{
    public override PageData PageData { get; protected set; } = new()
    {
        Title = "Data Services in the Clean Design Blazor Template",
        PublishDate = new DateOnly(2021, 11, 28),
        LastUpdated = new(2021, 11, 28),
        Author = "Shaun Curtis",
        Category = "Stories",
        Story= "Clean Design Template for Blazor",
        Order=30,
        Tags = "#Blazor;#CleanDesign;",
        Description = "Data Services in the Clean Design Blazor Template.",
        MdFile = $@".\Source\stories\Clean-Design-Template-for-Blazor\Clean-Design-Blazor-Template-DataServices.md",
    };
}